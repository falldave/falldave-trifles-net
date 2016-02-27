
namespace FallDave.Trifles.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using System.Xml.XPath;
    public static class XmlValueConversions
    {
        private XPathNavigator AsBasicTextElement(string value)
        {
            if(value == null)
            {
                throw new ArgumentNullException("value");
            }
            return new XElement("item", value).CreateNavigator();
        }

        public static XPathNavigator AsXPathString(string value)
        {
            

        }
    }
}
