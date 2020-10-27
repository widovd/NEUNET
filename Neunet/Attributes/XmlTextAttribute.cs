using System;

namespace Neunet.Attributes
{
    public sealed class XmlTextAttribute : Attribute
    {
        public string XmlText { get; private set; }

        public XmlTextAttribute(string xmlText) : base()
        {
            XmlText = xmlText;
        }
    }

}
