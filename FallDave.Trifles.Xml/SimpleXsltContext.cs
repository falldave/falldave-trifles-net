using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace FallDave.Trifles.Xml
{
    class SimpleXsltContext : XsltContext
    {
        public SimpleXsltContext()
        {
        }

        public SimpleXsltContext(NameTable nameTable) : base(nameTable)
        {
        }

        public override bool Whitespace { get { return true; } }

        public override int CompareDocument(string baseUri, string nextbaseUri)
        {
            throw new NotImplementedException();
        }

        public override bool PreserveWhitespace(XPathNavigator node)
        {
            return true;
        }

        /// <summary>
        /// Looks up the specified prefix and returns the result as an <see cref="XNamespace"/>.
        /// </summary>
        /// <param name="prefix">The prefix to look up (<see cref="string.Empty"/> for the default namespace).</param>
        /// <returns>An <see cref="XNamespace"/> corresponding to the namespace name to which the given prefix is mapped</returns>
        public XNamespace AsXNamespace(string prefix)
        {
            if(prefix == null)
            {
                throw new ArgumentNullException("prefix");
            }

            var namespaceName = LookupNamespace(prefix);
            return XNamespace.Get(namespaceName);
        }

        public XName AsXName(string prefix, string localName)
        {
            if(localName == null)
            {
                throw new ArgumentNullException("localName");
            }

            return AsXNamespace(prefix) + localName;
        }

        public override IXsltContextFunction ResolveFunction(string prefix, string localName, XPathResultType[] ArgTypes)
        {
            return ResolveFunction(AsXName(prefix, localName), ArgTypes);
        }

        public virtual IXsltContextFunction ResolveFunction(XName xName, params XPathResultType[] argTypes)
        {
            if(xName == null)
            {
                throw new ArgumentNullException("xName");
            }

            throw new NotImplementedException();
        }

        public override IXsltContextVariable ResolveVariable(string prefix, string localName)
        {
            return ResolveVariable(AsXName(prefix, localName));
        }

        public virtual IXsltContextVariable ResolveVariable(XName xName)
        {
            if (xName == null)
            {
                throw new ArgumentNullException("xName");
            }

            throw new NotImplementedException();
        }
    }
}
