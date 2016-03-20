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
            if (source == null) { throw new ArgumentNullException("source"); }
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
            foreach(var xns in GetXNamespaceOpt(namespaceResolver, prefix))
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
            if(localName == null) { throw new ArgumentNullException("localName"); }
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

            using (var reader = navigable.CreateNavigator().ReadSubtree())
            {
                // This bit is needed to avoid an error that says the XmlReader state isn't Interactive.
                reader.MoveToContent();

                return XNode.ReadFrom(reader);
            }
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

        
    }
}
