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
    public static class XmlExtensions
    {
        public static IEnumerable<XPathNavigator> AsEnumerable(this XPathNodeIterator source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            return source.Cast<XPathNavigator>();
        }

        public static Opt<string> LookupNamespaceOpt(this IXmlNamespaceResolver namespaceResolver, string prefix)
        {
            if (namespaceResolver == null) { throw new ArgumentNullException("namespaceResolver"); }
            if (prefix == null) { throw new ArgumentNullException("prefix"); }
            var namespaceName = namespaceResolver.LookupNamespace(prefix);
            return Opt.FullIfNotNull(namespaceName);
        }

        public static Opt<string> LookupPrefixOpt(this IXmlNamespaceResolver namespaceResolver, string namespaceName)
        {
            if (namespaceResolver == null) { throw new ArgumentNullException("namespaceResolver"); }
            if (namespaceName == null) { throw new ArgumentNullException("namespaceName"); }
            var prefix = namespaceResolver.LookupPrefix(namespaceName);
            return Opt.FullIfNotNull(prefix);
        }

        public static Opt<XNamespace> GetXNamespaceOpt(this IXmlNamespaceResolver namespaceResolver, string prefix)
        {
            if (namespaceResolver == null) { throw new ArgumentNullException("namespaceResolver"); }
            if (prefix == null) { throw new ArgumentNullException("prefix"); }
            return namespaceResolver.LookupNamespaceOpt(prefix).SelectFix(ns => XNamespace.Get(ns));
        }

        public static Opt<XName> GetXNameOpt(this IXmlNamespaceResolver namespaceResolver, string prefix, string localName)
        {
            if (namespaceResolver == null) { throw new ArgumentNullException("namespaceResolver"); }
            if (prefix == null) { throw new ArgumentNullException("prefix"); }
            if (localName == null) { throw new ArgumentNullException("localName"); }
            return namespaceResolver.GetXNamespaceOpt(prefix).SelectFix(xns => xns.GetName(localName));
        }

    }
}
