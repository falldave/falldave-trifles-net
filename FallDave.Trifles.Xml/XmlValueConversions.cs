
namespace FallDave.Trifles.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;

    public static class XmlValueConversions
    {
        private static readonly XPathExpression StringExpr = XPathExpression.Compile("string(.)");
        private static readonly XPathExpression NumberExpr = XPathExpression.Compile("number(.)");
        private static readonly XPathExpression BooleanExpr = XPathExpression.Compile("boolean(.)");

        public static XPathNavigator AsTextXPathNavigator(this string value)
        {
            if(value == null) { throw new ArgumentNullException("value"); }
            var doc = new XmlDocument();
            var element = doc.CreateElement("item");
            doc.AppendChild(element);
            var textNode = doc.CreateTextNode(value);
            element.AppendChild(textNode);
            return textNode.CreateNavigator();
        }

        private static T EvaluateAndCastExpression<T>(XPathNavigator value, XPathExpression expr)
        {
            if (value == null) { throw new ArgumentNullException("value"); }
            var result = value.Evaluate(expr);
            return (T)result;
        }

        public static string EvaluateStringFunction(this XPathNavigator value)
        {
            return EvaluateAndCastExpression<string>(value, StringExpr);
        }
        
        public static double EvaluateNumberFunction(this XPathNavigator value)
        {
            return EvaluateAndCastExpression<double>(value, NumberExpr);
        }

        public static bool EvaluateBooleanFunction(this XPathNavigator value)
        {
            return EvaluateAndCastExpression<bool>(value, BooleanExpr);
        }
    }
}
