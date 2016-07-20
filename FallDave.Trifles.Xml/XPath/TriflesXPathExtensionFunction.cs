using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace FallDave.Trifles.Xml.XPath
{
    /// <summary>
    /// Delegate type for functions that can be used to implement an XPath extension function.
    /// </summary>
    /// <param name="xsltContext"></param>
    /// <param name="args"></param>
    /// <param name="docContext"></param>
    /// <returns></returns>
    public delegate object TriflesXPathInvokable(XsltContext xsltContext, object[] args, XPathNavigator docContext);

    /// <summary>
    /// An XPath extension function with a qualified name.
    /// </summary>
    public class TriflesXPathExtensionFunction : IXsltContextFunction
    {
        private readonly TriflesXPathInvokable invokable;

        // Methods to access the private fields.

        /// <summary>
        /// The minimum number of parameters to be passed to this function.
        /// </summary>
        public int Minargs { get; private set; }

        /// <summary>
        /// The maximum number of parameters to be passed to this function, or <see
        /// cref="int.MaxValue"/> to indicate no limit.
        /// </summary>
        public int Maxargs { get; private set; }

        private ImmutableArray<XPathResultType> argTypes;

        /// <summary>
        /// The types of the parameters to be passed to this function. Parameters that accept any of
        /// multiple types should be specified as <see cref="XPathResultType.Any"/>. Optional
        /// parameters should be omitted.
        /// </summary>
        public XPathResultType[] ArgTypes { get { return argTypes.ToArray(); } }

        /// <summary>
        /// The type of the value to be returned from this function. Functions that return any of multiple types
        /// should be specified as <see cref="XPathResultType.Any"/>.
        /// </summary>
        public XPathResultType ReturnType { get; private set; }

        /// <summary>
        /// The qualified name by which this function is known.
        /// </summary>
        public XName FunctionName { get; private set; }


        /// <summary>
        /// Creates a new extension function definition.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="minArgs"></param>
        /// <param name="maxArgs"></param>
        /// <param name="argTypes"></param>
        /// <param name="returnType"></param>
        /// <param name="fn"></param>
        public TriflesXPathExtensionFunction(XName name, int minArgs, int maxArgs, XPathResultType[] argTypes, XPathResultType returnType, TriflesXPathInvokable fn)
        {
            FunctionName = Checker.NotNull(name, "name");

            Minargs = minArgs;
            Maxargs = maxArgs;

            if (Minargs < 0)
            {
                throw new ArgumentOutOfRangeException("minArgs");
            }
            else if (Maxargs < Minargs)
            {
                throw new ArgumentOutOfRangeException("maxArgs", "maxArgs cannot be less than minArgs");
            }

            if (argTypes == null)
            {
                argTypes = new XPathResultType[0];
            }

            this.argTypes = argTypes.ToImmutableArray();

            ReturnType = Checker.NotNull(returnType, "returnType");

            invokable = Checker.NotNull(fn, "fn");
        }

        /// <summary>
        /// Calls the actual function implemented by this function definition.
        /// </summary>
        /// <param name="xsltContext"></param>
        /// <param name="args"></param>
        /// <param name="docContext"></param>
        /// <returns></returns>
        public object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext)
        {
            return invokable(xsltContext, args, docContext);
        }
    }
}
