using System.Xml;

namespace Neulib.Serializers
{
    public interface IXmlDocSerializable
    {
        void WriteToXml(XmlElement element, XmlDocSerializer serializer);
    }

}
