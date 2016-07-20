using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace FallDave.Trifles.Xml.XPath
{
    /// <summary>
    /// A by-name reference to a variable in the given context.
    /// </summary>
    internal class DeferredVariable : IXsltContextVariable
    {
        private readonly TriflesXPathContext context;
        private readonly XName name;

        public DeferredVariable(TriflesXPathContext context, XName name)
        {
            this.context = context;
            this.name = Checker.NotNull(name, "name");
        }

        public Opt<TriflesXPathExtensionVariable> ResolvedOpt
        {
            get
            {
                return context.Table.GetVariableOpt(name);
            }
        }

        public bool IsLocal { get { return ResolvedOpt.Any(r => r.IsLocal); } }

        public bool IsParam { get { return ResolvedOpt.Any(r => r.IsParam); } }

        public XPathResultType VariableType
        {
            get
            {
                return ResolvedOpt.Select(r => r.VariableType).SingleOrValue(XPathResultType.Any);
            }
        }

        public object Evaluate(XsltContext xsltContext)
        {
            return ResolvedOpt.Select(r => r.Evaluate(xsltContext)).SingleOrDefault();
        }
    }
}
