//-----------------------------------------------------------------------
// <copyright file="XmlExtensions.cs" company="falldave">
//
// Written in 2015-2017 by David McFall
//
// To the extent possible under law, the author(s) have dedicated all copyright
// and related and neighboring rights to this software to the public domain
// worldwide. This software is distributed without any warranty.
//
// You should have received a copy of the CC0 Public Domain Dedication along
// with this software. If not, see
// [http://creativecommons.org/publicdomain/zero/1.0/].
//
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using FallDave.Trifles;

namespace FallDave.Trifles.Xml
{
    /// <summary>
    /// Adapters and converters among APIs in System.Xml, System.Xml.XPath, and System.Xml.Linq.
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// Wraps a nullary function returning a new navigator as an IXPathNavigable.
        /// </summary>
        /// <param name="createNavigatorFunction"></param>
        /// <returns></returns>
        public static IXPathNavigable AsXPathNavigable(this Func<XPathNavigator> createNavigatorFunction)
        {
            Checker.NotNull(createNavigatorFunction, "createNavigatorFunction");
            return new FunctionBasedXPathNavigable(createNavigatorFunction);
        }

        /// <summary>
        /// Adapts an XPathNodeIterator to serve as an IEnumerable{XPathNavigator}.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<XPathNavigator> AsEnumerable(this XPathNodeIterator source)
        {
            Checker.NotNull(source, "source");
            return source.Cast<XPathNavigator>();
        }

        /// <summary>
        /// Wraps an XNode as an IXPathNavigable.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IXPathNavigable AsXPathNavigable(this XNode source)
        {
            Checker.NotNull(source, "source");
            return AsXPathNavigable(() => source.CreateNavigator());
        }

        #region IXmlNamespaceResolver lookups for XName/XNamespace

        /// <summary>
        /// Retrieves, as an option, the namespace name in this namespace resolver corresponding to the specified prefix.
        /// If the prefix is unknown, the option will be empty.
        /// </summary>
        /// <param name="namespaceResolver"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static Opt<string> LookupNamespaceOpt(this IXmlNamespaceResolver namespaceResolver, string prefix)
        {
            if (namespaceResolver == null) { throw new ArgumentNullException("namespaceResolver"); }
            if (prefix == null) { throw new ArgumentNullException("prefix"); }
            var namespaceName = namespaceResolver.LookupNamespace(prefix);
            return Opt.FullIfNotNull(namespaceName);
        }

        /// <summary>
        /// Retrieves, as an option, a prefix in this namespace resolver corresponding to the specified namespace name.
        /// If the namespace name is unknown, the option will be empty.
        /// A namespace name may correspond to multiple prefixes, and this method is not guaranteed to return the same result from any two calls for the same input.
        /// </summary>
        /// <param name="namespaceResolver"></param>
        /// <param name="namespaceName"></param>
        /// <returns></returns>
        public static Opt<string> LookupPrefixOpt(this IXmlNamespaceResolver namespaceResolver, string namespaceName)
        {
            if (namespaceResolver == null) { throw new ArgumentNullException("namespaceResolver"); }
            if (namespaceName == null) { throw new ArgumentNullException("namespaceName"); }
            var prefix = namespaceResolver.LookupPrefix(namespaceName);
            return Opt.FullIfNotNull(prefix);
        }

        /// <summary>
        /// Retrieves, as an XNamespace in an option, the namespace name in this namespace resolver corresponding to the specified prefix.
        /// If the prefix is unknown, the option will be empty.
        /// </summary>
        /// <param name="namespaceResolver"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static Opt<XNamespace> GetXNamespaceOpt(this IXmlNamespaceResolver namespaceResolver, string prefix)
        {
            if (namespaceResolver == null) { throw new ArgumentNullException("namespaceResolver"); }
            if (prefix == null) { throw new ArgumentNullException("prefix"); }
            return namespaceResolver.LookupNamespaceOpt(prefix).SelectFix(ns => XNamespace.Get(ns));
        }

        /// <summary>
        /// Retrieves, as an XNamespace, the namespace name in this namespace resolver corresponding to the specified prefix.
        /// If the prefix is unknown, this throws an XslException.
        /// </summary>
        /// <param name="namespaceResolver"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static XNamespace GetRequiredXNamespace(this IXmlNamespaceResolver namespaceResolver, string prefix)
        {
            foreach (var xns in GetXNamespaceOpt(namespaceResolver, prefix))
            {
                return xns;
            }
            throw new System.Xml.Xsl.XsltException("The prefix '{0}' is not defined.".FormatStr(prefix));
        }

        /// <summary>
        /// Retrieves, as an XName in an option, the namespace name in this namespace resolver corresponding to the specified prefix, paired with the local name.
        /// If the prefix is unknown, the option will be empty.
        /// </summary>
        /// <param name="namespaceResolver"></param>
        /// <param name="prefix"></param>
        /// <param name="localName"></param>
        /// <returns></returns>
        public static Opt<XName> GetXNameOpt(this IXmlNamespaceResolver namespaceResolver, string prefix, string localName)
        {
            if (namespaceResolver == null) { throw new ArgumentNullException("namespaceResolver"); }
            if (prefix == null) { throw new ArgumentNullException("prefix"); }
            if (localName == null) { throw new ArgumentNullException("localName"); }
            return namespaceResolver.GetXNamespaceOpt(prefix).SelectFix(xns => xns.GetName(localName));
        }

        /// <summary>
        /// Retrieves, as an XName, the namespace name in this namespace resolver corresponding to the specified prefix, paired with the local name.
        /// If the prefix is unknown, this throws an XslException.
        /// </summary>
        /// <param name="namespaceResolver"></param>
        /// <param name="prefix"></param>
        /// <param name="localName"></param>
        /// <returns></returns>
        public static XName GetRequiredXName(this IXmlNamespaceResolver namespaceResolver, string prefix, string localName)
        {
            if (localName == null) { throw new ArgumentNullException("localName"); }
            return namespaceResolver.GetRequiredXNamespace(prefix).GetName(localName);
        }

        #endregion

        #region IXPathNavigable to XNode/XElement

        /// <summary>
        /// Converts the given navigable into an XNode.
        /// </summary>
        /// <param name="navigable"></param>
        /// <returns></returns>
        public static XNode ToXNode(this IXPathNavigable navigable)
        {
            Checker.NotNull(navigable, "navigable");

            using (var reader = navigable.CreateXmlReader())
            {
                // This bit is needed to avoid an error that says the XmlReader state isn't Interactive.
                reader.MoveToContent();

                return XNode.ReadFrom(reader);
            }
        }

        private static XmlReader CreateXmlReader(this IXPathNavigable navigable)
        {
            // Ideally, this would be navigable.CreateNavigator().ReadSubtree(), but doing that
            // appears to leave some important information out, including information about
            // namespaces. The following isn't super-efficient but it does work.
            return XmlReader.Create(new System.IO.StringReader(navigable.CreateNavigator().OuterXml));
        }



        /// <summary>
        /// Converts an XNode to an XElement if it can be read as an element.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static Opt<XElement> ToXElementOpt(this XNode node)
        {
            Checker.NotNull(node, "node");
            return Opt.FullIfNotNull(node as XElement);
        }

        /// <summary>
        /// Converts a navigable to an XElement if it can be read as an element.
        /// </summary>
        /// <param name="navigable"></param>
        /// <returns></returns>
        public static Opt<XElement> ToXElementOpt(this IXPathNavigable navigable)
        {
            return navigable.ToXNode().ToXElementOpt();
        }

        #endregion


        /// <summary>
        /// An XNamespace containing "http://www.w3.org/2001/XMLSchema-instance".
        /// </summary>
        public static readonly XNamespace XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";

        /// <summary>
        /// An XName containing "{http://www.w3.org/2001/XMLSchema-instance}nil", which is usually
        /// rendered as "xsi:nil".
        /// </summary>
        public static XName XsiNilName = XsiNamespace + "nil";

        /// <summary>
        /// Retrieves the attribute having the given name, or an empty result if there is no such attribute.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Opt<XAttribute> AttributeOpt(this XElement element, XName name)
        {
            Checker.NotNull(element, "element");
            Checker.NotNull(name, "name");
            return Opt.FullIfNotNull(element.Attribute(name));
        }

        /// <summary>
        /// Retrieves the value of the attribute having the given name, or an empty result if there
        /// is no such attribute.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Opt<string> AttributeValueOpt(this XElement element, XName name)
        {
            return element.AttributeOpt(name).SelectFix(a => a.Value);
        }

        /// <summary>
        /// Retrieves the value of the attribute having the given name, or an empty result if there is no such attribute.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Opt<string> AttributeValueOpt(this XmlElement element, XName name)
        {
            Checker.NotNull(element, "element");
            Checker.NotNull(name, "name");

            var hasValue = true;
            var value = element.RawGetAttribute(name);
            if (value.Equals(string.Empty))
            {
                // Since an empty string is returned for a missing attribute, we must check whether
                // the value is absent or simply blank.
                hasValue = element.RawHasAttribute(name);
            }
            return Opt.Create(hasValue, value);
        }

        /// <summary>
        /// Retrieves the value of the attribute having the given name, or an empty result if there is no such attribute.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Opt<string> AttributeValueOpt(this IXPathNavigable element, XName name)
        {
            Checker.NotNull(element, "element");
            Checker.NotNull(name, "name");

            var nav = element.GetSelfOrNewElementNavigator();

            var hasValue = true;
            var value = nav.RawGetAttribute(name);
            if (value.Equals(string.Empty))
            {
                // Since an empty string is returned for a missing attribute, we must check whether
                // the value is absent or simply blank.
                hasValue = nav.RawHasAttribute(name);
            }
            return Opt.Create(hasValue, value);
        }



        /// <summary>
        /// Determines whether this element contains an attribute matching the given XName.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool HasAttribute(this XmlElement element, XName name)
        {
            Checker.NotNull(element, "element");
            Checker.NotNull(name, "name");
            return element.RawHasAttribute(name);
        }

        /// <summary>
        /// Determines whether this element contains an attribute matching the given XName.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool HasAttribute(this IXPathNavigable element, XName name)
        {
            Checker.NotNull(element, "element");
            Checker.NotNull(name, "name");
            return element.GetSelfOrNewElementNavigator().RawHasAttribute(name);
        }


        /// <summary>
        /// Adds, removes, or sets an attribute. If <paramref name="value"/> is not null, the
        /// attribute's value will be <paramref name="value"/> after conversion to string. If the
        /// value is null and the attribute exists, it is removed. If the value is not null and the
        /// attribute does not exist, it is added.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetAttributeValue(this XmlElement element, XName name, object value)
        {
            Checker.NotNull(element, "element");
            Checker.NotNull(name, "name");

            if (value == null)
            {
                element.RawRemoveAttribute(name);
            }
            else
            {
                var stringValue = value.ToString();
                element.RawSetAttribute(name, stringValue);
            }
        }

        /// <summary>
        /// Adds, removes, or sets an attribute. If <paramref name="value"/> is not null, the
        /// attribute's value will be <paramref name="value"/> after conversion to string. If the
        /// value is null and the attribute exists, it is removed. If the value is not null and the
        /// attribute does not exist, it is added.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <exception cref="NotSupportedException">The given navigable does not support editing.</exception>
        public static void SetAttributeValue(this IXPathNavigable element, XName name, object value)
        {
            Checker.NotNull(element, "element");
            Checker.NotNull(name, "name");

            element.GetSelfOrNewElementNavigator().RawSetAttribute(name, (value == null) ? null : value.ToString());
        }



        /// <summary>
        /// Removes an attribute with the specified XName.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        public static void RemoveAttribute(this XmlElement element, XName name)
        {
            Checker.NotNull(element, "element");
            Checker.NotNull(name, "name");
            element.RawRemoveAttribute(name);
        }


        /// <summary>
        /// Removes an attribute with the specified XName.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <exception cref="NotSupportedException">The given navigable does not support editing.</exception>
        public static void RemoveAttribute(this IXPathNavigable element, XName name)
        {
            element.SetAttributeValue(name, null);
        }



        /// <summary>
        /// Retrieves the value of the attribute having the given name, or <c>null</c> if there is no such attribute.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string AttributeValue(this XElement element, XName name)
        {
            return element.AttributeValueOpt(name).SingleOrDefault();
        }

        /// <summary>
        /// Retrieves the value of the attribute having the given name, or <c>null</c> if there is no such attribute.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string AttributeValue(this XmlElement element, XName name)
        {
            return element.AttributeValueOpt(name).SingleOrDefault();
        }

        /// <summary>
        /// Retrieves the value of the attribute having the given name, or <c>null</c> if there is no such attribute.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string AttributeValue(this IXPathNavigable element, XName name)
        {
            return element.AttributeValueOpt(name).SingleOrDefault();
        }

        private static bool TestXsiNilValue(string value)
        {
            if (value == null)
            {
                return false;
            }
            else if (value == "true")
            {
                return true;
            }
            throw new InvalidOperationException("Attribute " + XsiNilName + " must be omitted or have the value 'true' (value was '" + value + "').");
        }

        private static string GetXsiNilValue(bool newValue)
        {
            return newValue ? "true" : null;
        }


        /// <summary>
        /// Returns whether the attribute whose name is contained in <see cref="XsiNilName"/> exists
        /// and contains the string "true".
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool XsiNil(this XElement element)
        {
            return TestXsiNilValue(element.AttributeValue(XsiNilName));
        }

        /// <summary>
        /// Returns whether the attribute whose name is contained in <see cref="XsiNilName"/> exists
        /// and contains the string "true".
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool XsiNil(this XmlElement element)
        {
            return TestXsiNilValue(element.AttributeValue(XsiNilName));
        }

        /// <summary>
        /// Returns whether the attribute whose name is contained in <see cref="XsiNilName"/> exists
        /// and contains the string "true".
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool XsiNil(this IXPathNavigable element)
        {
            return TestXsiNilValue(element.AttributeValue(XsiNilName));
        }

        /// <summary>
        /// Sets whether the attribute whose name is contained in <see cref="XsiNilName"/> is set to
        /// the string "true" (if <c>true</c>) or omitted entirely (if <c>false</c>).
        /// </summary>
        /// <param name="element"></param>
        /// <param name="newValue"></param>
        public static void XsiNil(this XElement element, bool newValue)
        {
            element.SetAttributeValue(XsiNilName, GetXsiNilValue(newValue));
        }

        /// <summary>
        /// Sets whether the attribute whose name is contained in <see cref="XsiNilName"/> is set to
        /// the string "true" (if <c>true</c>) or omitted entirely (if <c>false</c>).
        /// </summary>
        /// <param name="element"></param>
        /// <param name="newValue"></param>
        public static void XsiNil(this XmlElement element, bool newValue)
        {
            element.SetAttributeValue(XsiNilName, GetXsiNilValue(newValue));
        }

        /// <summary>
        /// Sets whether the attribute whose name is contained in <see cref="XsiNilName"/> is set to
        /// the string "true" (if <c>true</c>) or omitted entirely (if <c>false</c>).
        /// </summary>
        /// <param name="element"></param>
        /// <param name="newValue"></param>
        /// <exception cref="NotSupportedException">The given navigable does not support editing.</exception>
        public static void XsiNil(this IXPathNavigable element, bool newValue)
        {
            element.SetAttributeValue(XsiNilName, GetXsiNilValue(newValue));
        }

        #region AbsolutePath implementation

        /// <summary>
        /// Determines a generic XPath to the given navigable.
        /// </summary>
        /// <para>
        /// The resulting path evaluated against the original document will select the node indicated
        /// by <paramref name="navigable"/>; on other documents, results may vary. With the exception
        /// of the final path component, all path components refer to the name of an element. If a
        /// path component would match more than one of a set of siblings with similar features, a
        /// numeric index is appended.
        /// </para>
        /// <param name="navigable"></param>
        /// <returns></returns>
        public static string AbsolutePath(this IXPathNavigable navigable)
        {
            var nav = navigable.CreateNavigator();

            var components = new List<string>();

            while (ComputeXPathComponent(nav, components))
            {
                nav.MoveToParent();
            }

            if (components.Count == 0)
            {
                return "/";
            }

            components.Add(""); // to represent root
            components.Reverse(); // make big-endian
            return components.JoinWithStr("/");
        }

        // Returns the given string in single quotes, if it contains no single quotes; otherwise,
        // returns a "concat()" expression that evaluates to the string value.
        private static string QuoteXPathString(string str)
        {
            if (str.Contains("'"))
            {
                return "concat('" + str.Replace("'", "',\"'\",'") + "')";
            }
            return "'" + str + "'";
        }

        // Generates an XPath path component that references the current node uniquely in relation to
        // its parent node.
        private static bool ComputeXPathComponent(XPathNavigator nav, List<string> components)
        {
            string basicAddress;

            switch (nav.NodeType)
            {
                case XPathNodeType.Root:
                    return false;

                case XPathNodeType.Element:
                    basicAddress = "*[" + GetXPathNameCondition(nav) + "]";
                    break;

                case XPathNodeType.Attribute:
                    basicAddress = "@*[" + GetXPathNameCondition(nav) + "]";
                    break;

                case XPathNodeType.Text:
                case XPathNodeType.Whitespace:
                case XPathNodeType.SignificantWhitespace:
                    basicAddress = "text()[.=" + QuoteXPathString(nav.Value) + "]";
                    break;

                case XPathNodeType.Comment:
                    basicAddress = "comment()[.=" + QuoteXPathString(nav.Value) + "]";
                    break;

                case XPathNodeType.ProcessingInstruction:
                    basicAddress = "processing-instruction()[.=" + QuoteXPathString(nav.Value) + "]";
                    break;

                default:
                    basicAddress = "node()[.=" + QuoteXPathString(nav.Value) + "]";
                    break;
            }

            // If ordering is applicable, determine the proper index.
            if (nav.NodeType != XPathNodeType.Attribute)
            {
                var count = (double)nav.Evaluate("count(preceding-sibling::" + basicAddress + ")");
                basicAddress += "[" + (count + 1.0) + "]";
            }

            // Does the given address match this node?
            var selfAddress = "self::" + basicAddress;
            var res = nav.SelectSingleNode(selfAddress);
            if (res == null)
            {
                throw new Exception("Internal problem - A node with the type " + nav.NodeType + " was not matched using self-address " + selfAddress);
            }
            else {
                var pos = res.ComparePosition(nav);
                if (pos != XmlNodeOrder.Same)
                {
                    throw new Exception("Internal problem - A node with the type " + nav.NodeType + " was matched incorrectly using self-address " + selfAddress);
                }
            }

            components.Add(basicAddress);
            return true;
        }

        // Get a conditional expression that matches the current node by local name
        // and namespace name (not prefix-dependent).
        private static string GetXPathNameCondition(XPathNavigator nav)
        {
            return GetXPathNameCondition(nav.LocalName, nav.NamespaceURI);
        }

        // Get a conditional expression that matches a node with the given local name and namespace
        // name (not prefix-dependent).
        private static string GetXPathNameCondition(XName name)
        {
            return GetXPathNameCondition(name.LocalName, name.NamespaceName);
        }

        // Get a conditional expression that matches a node with the given local name and namespace
        // name (not prefix-dependent).
        private static string GetXPathNameCondition(string localName, string namespaceUri)
        {
            return "local-name()=" + QuoteXPathString(localName) + " and namespace-uri()=" + QuoteXPathString(namespaceUri);
        }

        #endregion


        // Given a navigable, cast it if it is an XPathNavigator or get .CreateNavigator() otherwise.
        // Avoid altering the navigator returned.
        private static XPathNavigator GetSelfOrNewNavigator(this IXPathNavigable navigable, string paramName = "navigable")
        {
            Checker.NotNull(navigable, paramName);
            var nav = navigable as XPathNavigator;
            return (nav == null) ? navigable.CreateNavigator() : nav;
        }

        // Same as GetSelfOrNewNavigator, but also verifies that the result is parked on an element node.
        private static XPathNavigator GetSelfOrNewElementNavigator(this IXPathNavigable element, string paramName = "element")
        {
            var nav = element.GetSelfOrNewNavigator(paramName);
            if (nav.NodeType != XPathNodeType.Element)
            {
                throw new ArgumentException("Operation not valid on non-element node (" + nav.NodeType.ToString() + ")", paramName);
            }
            return nav;
        }

        #region Some wrappers

        private static string RawGetAttribute(this XmlElement element, XName name)
        {
            // Empty string is returned if there is no such attribute.
            return element.GetAttribute(name.LocalName, name.NamespaceName);
        }

        private static bool RawHasAttribute(this XmlElement element, XName name)
        {
            return element.HasAttribute(name.LocalName, name.NamespaceName);
        }

        private static void RawRemoveAttribute(this XmlElement element, XName name)
        {
            element.RemoveAttribute(name.LocalName, name.NamespaceName);
        }

        private static string RawSetAttribute(this XmlElement element, XName name, string value)
        {
            return element.SetAttribute(name.LocalName, name.NamespaceName, value);
        }

        private static string RawGetAttribute(this XPathNavigator element, XName name)
        {
            // Empty string is returned if there is no such attribute.
            return element.GetAttribute(name.LocalName, name.NamespaceName);
        }

        private static XPathNavigator MoveCloneToAttribute(this XPathNavigator element, XName name)
        {
            var nav = element.Clone();
            if (nav.MoveToAttribute(name.LocalName, name.NamespaceName))
            {
                return nav;
            }
            return null;
        }

        private static bool RawHasAttribute(this XPathNavigator element, XName name)
        {
            return element.MoveCloneToAttribute(name) != null;
        }

        private static void RawSetAttribute(this XPathNavigator element, XName name, string value)
        {
            var attNav = element.MoveCloneToAttribute(name);
            var hasAttribute = attNav != null;

            if (value == null)
            {
                if (hasAttribute)
                {
                    attNav.DeleteSelf();
                }
                // Otherwise, the attribute didn't exist anyway.
            }
            else
            {
                if (hasAttribute)
                {
                    // Update existing attribute
                    attNav.SetValue(value);
                }
                else
                {
                    // Create new attribute
                    element.CreateAttribute(null, name.LocalName, name.NamespaceName, value);
                }
            }
        }



        #endregion
    }
}
