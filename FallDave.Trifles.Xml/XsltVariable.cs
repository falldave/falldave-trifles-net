using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace FallDave.Trifles.Xml
{
    class XsltVariable : IXsltContextVariable
    {
        public bool IsLocal
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsParam
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public XPathResultType VariableType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public object Evaluate(XsltContext xsltContext)
        {
            throw new NotImplementedException();
        }
    }
}
