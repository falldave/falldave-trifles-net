using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace FallDave.Trifles.Xml.XPath
{
    /// <summary>
    /// Helpers for XPath data conversion, coercion, and evaluation.
    /// </summary>
    public static class TriflesXPathData
    {
        /// <summary>
        /// Traverses the given list, converting each element to the form <c><![CDATA[<item
        /// type="...">...</item>]]></c> and returns the resulting nodes as a node-set.
        /// </summary>
        /// <para>The possible node types:</para>
        /// <list>
        /// <item>
        /// If the value is a <c>bool</c>, the type attribute is <c>boolean</c>. The element contains
        /// the text <c>true</c> for a true value, or contains nothing for a false value. Passing the
        /// node or string value to the XPath <c>boolean()</c> function produces the correct result.
        /// </item>
        /// <item>
        /// If the value is numeric, the type attribute is <c>boolean</c>. Since XPath numeric values
        /// correspond specifically to the <c>double</c> type, numeric values are converted to
        /// <c>double</c> s if necessary. Passing the node or string value to the XPath
        /// <c>number()</c> function produces the correct result.
        /// </item>
        /// <item>
        /// If the value is a <c>string</c> or a <c>char</c>, the type attribute is <c>string</c>.
        /// Passing the node value to the XPath <c>string()</c> function produces the correct result.
        /// </item>
        /// <item>
        /// If the value is <c>null</c>, the type attribute is <c>null</c>. XPath has no direct
        /// equivalent to <c>null</c>. However, the element contains nothing (i.e. an empty
        /// node-set), and converts to boolean <c>false</c>, number <c>NaN</c>, or the empty string,
        /// making it a suitable non-value for many situations.
        /// </item>
        /// <item>
        /// If the value is an <c>XPathNodeIterator</c>, the type attribute is <c>node-set</c>. The
        /// element contains a copy of the iterator's content.
        /// </item>
        /// <item>
        /// If the value is an <c>IXPathNavigable</c> (such as <c>XPathNavigator</c>), the type
        /// attribute is <c>result-tree-fragment</c>. The element contains a copy of the navigator's content.
        /// </item>
        /// </list>
        /// <para>
        /// Values of types other than specified above are rejected. Unsupported values should be
        /// converted to an XML node-set or one of the supported scalar types.
        /// </para>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static XPathNodeIterator ToNodeSet(IEnumerable<object> elements)
        {
            var items = elements.Select(element => ToNodeSetItem(element));
            var root = new XElement("root", items.ToArray());
            var rootNav = root.CreateNavigator();
            var result = (XPathNodeIterator)rootNav.Evaluate("item");
            return result;
        }



        // Converts a value to a node-set item.
        private static XElement ToNodeSetItem(object value)
        {
            if (value is bool)
            {
                var boolValue = (bool)value;
                return boolValue ? CreateNodeSetItemElement("boolean", "true") : CreateNodeSetItemElement("boolean");
            }
            else if (value is char)
            {
                return CreateNodeSetItemElement("string", ((char)value).ToString());
            }
            else if (value is IConvertible && ((IConvertible)value).IsNumericAndNotChar())
            {
                var convValue = (IConvertible)value;
                var numericValue = Convert.ToDouble(convValue);
                var xpathStringValue = ToString(numericValue);

                return CreateNodeSetItemElement("number", xpathStringValue);
            }
            else if (value is string)
            {
                return CreateNodeSetItemElement("string", value);
            }
            else if (value is XPathNodeIterator)
            {
                var i = (XPathNodeIterator)value;
                var nodes = i.AsEnumerable().Select(n => n.ToXNode()).ToArray();

                return CreateNodeSetItemElement("node-set", nodes);
            }
            else if (value is IXPathNavigable)
            {
                var xnode = ((IXPathNavigable)value).ToXNode();

                return CreateNodeSetItemElement("result-tree-fragment", xnode);
            }
            else if (value == null)
            {
                return CreateNodeSetItemElement("null");
            }
            else
            {
                throw new XPathException("Converting value of type " + value.GetType().FullName + " is not supported.");
            }
        }

        // Creates an XElement containing the data of a node-set item.
        private static XElement CreateNodeSetItemElement(string type, params object[] otherContent)
        {
            var typeAttr = new XAttribute("type", type);
            var elem = new XElement("item", typeAttr);
            elem.Add(otherContent);
            return elem;
        }

        // Creates a navigator on a new document consisting of only a root node with a generated name.
        private static XPathNavigator GetDummyNavigator()
        {
            var elementName = "_DUMMY-" + Guid.NewGuid().ToString();
            var nav = new XElement(elementName).CreateNavigator();
            return nav;
        }

        /// <summary>
        /// Returns the indicated navigator or, if the navigator is <c>null</c> or omitted, a new
        /// dummy navigator.
        /// </summary>
        /// <para>
        /// If a dummy navigator is produced, the navigator is over a single, empty <seealso
        /// cref="XElement"/> with an arbitrary GUID-based name.
        /// </para>
        /// <param name="nav"></param>
        /// <returns></returns>
        public static XPathNavigator EnsureNavigator(XPathNavigator nav = null)
        {
            return nav ?? GetDummyNavigator();
        }

        /// <summary>
        /// Produces an empty node-set.
        /// </summary>
        /// <para>
        /// The result of this method is obtained by evaluating the XPath expression <c>/..</c>,
        /// which always returns an empty node-set, on the supplied navigator (or, if <paramref
        /// name="nav"/> is <c>null</c>, a dummy navigator).
        /// </para>
        /// <param name="nav"></param>
        /// <returns></returns>
        public static XPathNodeIterator GetEmptyNodeSet(XPathNavigator nav = null)
        {
            // The expression for parent of / results in an empty set.
            return (XPathNodeIterator)EnsureNavigator(nav).Evaluate("/..");
        }

        /// <summary>
        /// Converts the parameter to <c>bool</c> by evaluating the XPath <c>boolean()</c> function on the value.
        /// </summary>
        /// <param name="param"></param>
        /// <param name="nav"></param>
        /// <returns></returns>
        private static bool ToBooleanForced(object param, XPathNavigator nav = null)
        {
            return (bool)Evaluate(nav, "boolean($param0)", param);
        }

        /// <summary>
        /// Converts the parameter to <c>double</c> by evaluating the XPath <c>number()</c> function on the value.
        /// </summary>
        /// <param name="param"></param>
        /// <param name="nav"></param>
        /// <returns></returns>
        private static double ToNumberForced(object param, XPathNavigator nav = null)
        {
            return (double)Evaluate(nav, "number($param0)", param);
        }

        /// <summary>
        /// Converts the parameter to <c>string</c> by evaluating the XPath <c>string()</c> function on the value.
        /// </summary>
        /// <param name="param"></param>
        /// <param name="nav"></param>
        /// <returns></returns>
        private static string ToStringForced(object param, XPathNavigator nav = null)
        {
            return (string)Evaluate(nav, "string($param0)", param);
        }


        /// <summary>
        /// If the parameter is a <c>bool</c>, returns the parameter; otherwise, converts the
        /// parameter to <c>bool</c> by evaluating the XPath <c>boolean()</c> function on the value.
        /// </summary>
        /// <param name="param"></param>
        /// <param name="nav"></param>
        /// <returns></returns>
        public static bool ToBoolean(object param, XPathNavigator nav = null)
        {
            return param is bool ? (bool)param : ToBooleanForced(param, nav);
        }

        /// <summary>
        /// If the parameter is a <c>double</c>, returns the parameter; if the parameter is a simple
        /// numeric type, returns the parameter converted by <seealso
        /// cref="Convert.ToDouble(object)"/>; otherwise, converts the parameter to <c>double</c> by
        /// evaluating the XPath <c>number()</c> function on the value.
        /// </summary>
        /// <param name="param"></param>
        /// <param name="nav"></param>
        /// <returns></returns>
        public static double ToNumber(object param, XPathNavigator nav = null)
        {
            if (param is double)
            {
                return (double)param;
            }
            else if (param is IConvertible && ((IConvertible)param).IsNumericAndNotChar())
            {
                return Convert.ToDouble(param);
            }
            else
            {
                return ToNumberForced(param, nav);
            }
        }

        /// <summary>
        /// If the parameter is a <c>string</c>, returns the parameter; otherwise, converts the
        /// parameter to <c>string</c> by evaluating the XPath <c>string()</c> function on the value.
        /// </summary>
        /// <param name="param"></param>
        /// <param name="nav"></param>
        /// <returns></returns>
        public static string ToString(object param, XPathNavigator nav = null)
        {
            return param is string ? (string)param : ToStringForced(param, nav);
        }




        /// <summary>
        /// Evaluates an XPath expression in a newly created context, making additional parameters available to the expression.
        /// </summary>
        /// <param name="nav">
        /// Navigator indicating the current node; if <c>null</c>, a dummy navigator is used.
        /// </param>
        /// <param name="xpathExpr">XPath expression to evaluate.</param>
        /// <param name="param">
        /// Values to be available while evaluating the expression as <c>$paramN</c>, where N is the
        /// index within this array.
        /// </param>
        /// <returns>The result of evaluating the expression</returns>
        public static object Evaluate(XPathNavigator nav, string xpathExpr, params object[] param)
        {
            Checker.NotNull(xpathExpr, "xpathExpr");

            nav = EnsureNavigator(nav);

            var expr = nav.Compile(xpathExpr);

            var context = new TriflesXPathContext();

            int i = 0;
            foreach (var paramN in param)
            {
                context.AddVariable(XNamespace.None + ("param" + i), paramN);
                i++;
            }

            expr.SetContext(context);
            return nav.Evaluate(expr);
        }

        /// <summary>
        /// Evaluates an XPath expression in a newly created context, making additional parameters available to the expression.
        /// The current node is provided by a dummy navigator.
        /// </summary>
        /// <param name="xpathExpr">XPath expression to evaluate.</param>
        /// <param name="param">
        /// Values to be available while evaluating the expression as <c>$paramN</c>, where N is the
        /// index within this array.
        /// </param>
        /// <returns>The result of evaluating the expression</returns>
        public static object Evaluate(string xpathExpr, params object[] param)
        {
            return Evaluate(null, xpathExpr, param);
        }

        /// <summary>
        /// Attempts to determine a closely matching <see cref="XPathResultType"/> for a given value.
        /// </summary>
        /// <para>
        /// <list>
        /// <item>If the value is a <see cref="bool"/>, returns <see cref="XPathResultType.Boolean"/>.</item>
        /// <item>If the value is a <see cref="string"/> or a <see cref="char"/>, returns <see cref="XPathResultType.String"/>.</item>
        /// <item>
        /// If the value is of any of the simple C# numeric types (excluding <see cref="char"/>),
        /// returns <see cref="XPathResultType.Number"/>.
        /// </item>
        /// <item>If the value is an <see cref="XPathNodeIterator"/>, returns <see cref="XPathResultType.NodeSet"/>.</item>
        /// <item>If the value is an <see cref="IXPathNavigable"/>, returns <see cref="XPathResultType.Navigator"/>.</item>
        /// <item>Otherwise, returns <see cref="XPathResultType.Any"/>.</item>
        /// </list>
        /// </para>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XPathResultType GuessXPathResultType(object value)
        {
            if (value is bool)
            {
                return XPathResultType.Boolean;
            }
            else if ((value is string) || (value is char))
            {
                return XPathResultType.String;
            }
            else if ((value is IConvertible) && ((IConvertible)value).IsNumericAndNotChar())
            {
                return XPathResultType.Number;
            }
            else if (value is XPathNodeIterator)
            {
                return XPathResultType.NodeSet;
            }
            else if (value is IXPathNavigable)
            {
                return XPathResultType.Navigator;
            }
            else
            {
                return XPathResultType.Any;
            }
        }
    }
}
