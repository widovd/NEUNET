using System.Xml;

namespace Neulib.Serializers
{
    public interface IXmlDocSerializable
    {
        //void ReadFromXml(XmlElement element, XmlDocSerializer serializer);
        void WriteToXml(XmlElement element, XmlDocSerializer serializer);
    }

}
