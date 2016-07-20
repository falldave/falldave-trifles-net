using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace FallDave.Trifles.Xml.XPath
{
    /// <summary>
    /// An XsltContext for use with XPath expressions defined in terms of
    /// <seealso cref="TriflesXPathExtensionFunction"/> and <see
    /// cref="TriflesXPathExtensionVariable"/>.
    /// </summary>
    public class TriflesXPathContext : XsltContext
    {
        private readonly XPathXNamesTable table;
        
        /// <summary>
        /// The <see cref="XPathXNamesTable"/> containing the functions and values for this context.
        /// </summary>
        public XPathXNamesTable Table { get { return table; } }

        /// <summary>
        /// Creates a new context.
        /// </summary>
        /// <param name="parent"></param>
        public TriflesXPathContext(TriflesXPathContext parent = null)
        {
            this.table = CreateChildTable(parent);
        }

        /// <summary>
        /// Creates a new context using the given name table.
        /// </summary>
        /// <param name="nt"></param>
        /// <param name="parent"></param>
        public TriflesXPathContext(NameTable nt, TriflesXPathContext parent = null) : base(nt)
        {
            this.table = CreateChildTable(parent);
        }

        private static XPathXNamesTable CreateChildTable(TriflesXPathContext parent)
        {
            return new XPathXNamesTable((parent != null) ? parent.table : null);
        }

        /// <summary>
        /// Retrieves a function within this context having the given prefix and name. (The argument
        /// types list is currently ignored.)
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="localName"></param>
        /// <param name="ArgTypes"></param>
        /// <returns></returns>
        public override IXsltContextFunction ResolveFunction(string prefix, string localName, XPathResultType[] ArgTypes)
        {
            return this
                .GetXNameOpt(prefix, localName)
                .Select(xname => table.GetFunctionOpt(xname))
                .SelectMany(x => x)
                .SingleOrDefault();
        }

        /// <summary>
        /// Retrieves a variable referring to the given name within this context.
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="localName"></param>
        /// <returns></returns>
        public override IXsltContextVariable ResolveVariable(string prefix, string localName)
        {
            return this
                .GetXNameOpt(prefix, localName)
                .Select(xname => new DeferredVariable(this, xname))
                .SingleOrDefault();
        }
        

        /// <summary>
        /// Always returns 0, indicating equal (refer to <see
        /// cref="XsltContext.CompareDocument(string, string)"/> for the general definition).
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="nextbaseUri"></param>
        /// <returns></returns>
        public override int CompareDocument(string baseUri, string nextbaseUri)
        {
            return 0;
        }
               
        /// <summary>
        /// Always returns <c>true</c>, meaning the given whitespace node is not to be stripped
        /// (refer to <see cref="XsltContext.PreserveWhitespace(XPathNavigator)"/> for the general definition).
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override bool PreserveWhitespace(XPathNavigator node)
        {
            return true;
        }

        /// <summary>
        /// Always returns <c>true</c>, meaning whitespace nodes are not stripped from input (refer
        /// to <see cref="XsltContext.Whitespace"/> for the general definition).
        /// </summary>
        public override bool Whitespace { get { return true; } }

        /// <summary>
        /// Add a variable to this context's table.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="isLocal"></param>
        /// <param name="isParam"></param>
        /// <param name="resultType"></param>
        public void AddVariable(XName name, object value, bool isLocal = false, bool isParam = false, XPathResultType? resultType = null)
        {
            var variable = new TriflesXPathExtensionVariable(name, value, isLocal, isParam, resultType);
            Table.AddVariable(variable);
        }
    }
}
