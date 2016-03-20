using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            return 0;
        }

        public override bool PreserveWhitespace(XPathNavigator node)
        {
            return true;
        }

        public override IXsltContextFunction ResolveFunction(string prefix, string localName, XPathResultType[] argTypes)
        {
            return ResolveFunction(this.GetRequiredXName(prefix, localName), argTypes);
        }

        public virtual IXsltContextFunction ResolveFunction(XName xName, params XPathResultType[] argTypes)
        {
            if (xName == null)
            {
                throw new ArgumentNullException("xName");
            }

            throw new NotImplementedException();
        }

        public override IXsltContextVariable ResolveVariable(string prefix, string localName)
        {
            return ResolveVariable(this.GetRequiredXName(prefix, localName));
        }

        public virtual IXsltContextVariable ResolveVariable(XName xName)
        {
            if (xName == null)
            {
                throw new ArgumentNullException("xName");
            }

            throw new NotImplementedException();
        }

        private class SimpleXsltFunction : IXsltContextFunction
        {
            private readonly ImmutableArray<XPathResultType> argTypes;
            private readonly Func<XsltContext, XPathNavigator, object[], object> implementation;
            private readonly int maxArgs;
            private readonly int minArgs;
            private readonly XPathResultType returnType;

            public SimpleXsltFunction(Func<XsltContext, XPathNavigator, object[], object> implementation,  XPathResultType returnType, int minArgs, int maxArgs, params XPathResultType[] argTypes)
            {
                if (implementation == null) throw new ArgumentNullException("implementation");
                if (minArgs < 0) throw new ArgumentOutOfRangeException("minArgs");
                if (maxArgs < minArgs) throw new ArgumentOutOfRangeException("maxArgs");
                if (argTypes == null) throw new ArgumentNullException("argTypes");

                this.implementation = implementation;
                this.returnType = returnType;
                this.minArgs = minArgs;
                this.maxArgs = maxArgs;
                this.argTypes = argTypes.ToImmutableArray();
            }

            public XPathResultType[] ArgTypes { get { return argTypes.ToArray(); } }

            public int Maxargs { get { return maxArgs; } }

            public int Minargs { get { return minArgs; } }

            public XPathResultType ReturnType { get { return returnType; } }

            public object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
            {
                return implementation(xsltContext, docContext, args);
            }
        }
    }
}
