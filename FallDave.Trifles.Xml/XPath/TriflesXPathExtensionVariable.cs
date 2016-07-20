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
    /// An immutable XPath extension variable.
    /// </summary>
    public class TriflesXPathExtensionVariable : IXsltContextVariable
    {
        /// <summary>
        /// Creates the variable object.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="isLocal"></param>
        /// <param name="isParam"></param>
        /// <param name="resultType"></param>
        public TriflesXPathExtensionVariable(XName name, object value, bool isLocal = false, bool isParam = false, XPathResultType? resultType = null)
        {
            this.VariableName = name;
            this.Value = value;
            this.IsLocal = isLocal;
            this.IsParam = isParam;

            this.VariableType = resultType.HasValue ? resultType.Value : TriflesXPathData.GuessXPathResultType(value);
        }

        /// <summary>
        /// Returns the value of this variable. The context parameter is ignored.
        /// </summary>
        /// <param name="xsltContext"></param>
        /// <returns></returns>
        public object Evaluate(XsltContext xsltContext)
        {
            return Value;
        }

        /// <summary>
        /// Returns the self-identified name of this variable.
        /// </summary>
        public XName VariableName { get; private set; }

        /// <summary>
        /// Returns the value of this variable.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Returns the value of the <c>isLocal</c> flag passed to the constructor.
        /// </summary>
        public bool IsLocal { get; private set; }

        /// <summary>
        /// Returns the value of the <c>isParam</c> flag passed to the constructor.
        /// </summary>
        public bool IsParam { get; private set; }

        /// <summary>
        /// Returns the self-identified XPath type of this variable's value; if none was passed to
        /// the constructor, the type was guessed based on the value's type (see <see
        /// cref="TriflesXPathData.GuessXPathResultType(object)"/> for details).
        /// </summary>
        public XPathResultType VariableType { get; private set; }
    }
}
