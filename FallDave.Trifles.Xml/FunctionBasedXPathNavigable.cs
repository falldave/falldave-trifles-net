using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace FallDave.Trifles.Xml
{
    /// <summary>
    /// Wraps a navigator-creating function as an IXPathNavigable.
    /// </summary>
    internal class FunctionBasedXPathNavigable : IXPathNavigable
    {
        private readonly Func<XPathNavigator> createNavigatorFunction;

        public FunctionBasedXPathNavigable(Func<XPathNavigator> createNavigatorFunction)
        {
            this.createNavigatorFunction = Checker.NotNull(createNavigatorFunction, "createNavigatorFunction");
        }

        public XPathNavigator CreateNavigator()
        {
            return createNavigatorFunction();
        }
    }
}
