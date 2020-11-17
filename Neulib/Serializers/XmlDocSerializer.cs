using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml;
using Neulib.Exceptions;

namespace Neulib.Serializers
{

    public sealed class XmlDocSerializer : Serializer
    {
        // ----------------------------------------------------------------------------------------
        #region Properties
        public string NameSpace { get; set; }

        public string SchemaInstance { get; set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public XmlDocSerializer(CancellationTokenSource tokenSource) : base(tokenSource)
        {
            NameSpace = @"https://www.signify.com/global";
            SchemaInstance = @"http://www.w3.org/2001/XMLSchema-serializable";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region TextReader / TextWriter

        private const string _TypeAttributeId = "type";

        public IXmlDocSerializable ReadValueChild(XmlElement childElement)
        {
            string typeName = childElement.GetAttribute(_TypeAttributeId);
            if (string.IsNullOrEmpty(typeName)) return null; // Happens when null is serialized.
            int token = int.Parse(typeName);
            Type type = Types.GetType(token);
            if (type == null) throw new VarNullException(nameof(type), 275854);
            IXmlDocSerializable serializable;
            try
            {
                serializable = Activator.CreateInstance(type, childElement, this) as IXmlDocSerializable;
            }
            catch (MissingMethodException ex)
            {
                throw new InvalidCodeException($"Activator.CreateInstance({type}) failed.", ex, 619405);
            }
            return serializable;
        }

        public IXmlDocSerializable ReadValue(XmlElement element, string name)
        {
            CancellationTokenSource?.Token.ThrowIfCancellationRequested();
            XmlElement childElement = element.GetChildElement(name);
            IXmlDocSerializable serializable = childElement != null ? ReadValueChild(childElement) : null;
            return serializable;
        }

        public void WriteValueChild(XmlElement childElement, IXmlDocSerializable serializable)
        {
            Type type = serializable.GetType();
            int token = Types.GetToken(type);
            string typeName = token.ToString();
            if (string.IsNullOrEmpty(typeName))
                throw new InvalidValueException($"Type '{serializable.GetType()}' is not registered in TypesDictionary", 178884);
            //throw XmlDocumentSerializationException.NotRegistered(serializable);
            childElement.SetAttribute(_TypeAttributeId, typeName);
            serializable.WriteToXml(childElement, this);
        }

        public void WriteValue(XmlElement element, string name, IXmlDocSerializable serializable)
        {
            CancellationTokenSource?.Token.ThrowIfCancellationRequested();
            XmlElement childElement = element.CreateChildElement(name);
            if (serializable != null) WriteValueChild(childElement, serializable);
        }


        private const string _RootId = "Root";
        private const string _ItemsId = "Items"; // Same as Neunet.BaseList._ItemsId. Don't know how to combine this.
        private const string _VersionId = "Version";

        public override object Deserialize(Stream stream)
        {
            XmlDocument document = new XmlDocument();
            document.Load(stream);
            XmlElement rootElement = document[_RootId];
            if (rootElement == null) throw new XmlException($"Root element '{_RootId}' is missing.");
            Version = Version.Parse(rootElement.GetAttribute(_VersionId.ToLower()));
            IXmlDocSerializable serializable = ReadValue(rootElement, _ItemsId);
            return serializable;
        }

        public override void Serialize(Stream stream, object graph)
        {
            if (!(graph is IXmlDocSerializable serializable)) return;
            XmlDocument document = new XmlDocument();
            XmlElement rootElement = document.CreateChildElement(_RootId);
            rootElement.SetAttribute("xmlns:xsd", NameSpace);
            rootElement.SetAttribute("xmlns:xsi", SchemaInstance);
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "utf-8", null);
            document.InsertBefore(declaration, rootElement);
            rootElement.SetAttribute(_VersionId.ToLower(), Version.ToString());
            WriteValue(rootElement, _ItemsId, serializable);
            document.Save(stream);
        }

        #endregion
    }
}
