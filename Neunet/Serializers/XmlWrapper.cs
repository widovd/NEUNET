using System;
using System.IO;
using System.Xml;
using Neulib.Serializers;

namespace Neunet.Serializers
{
    public class XmlWrapper
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public XmlDocument Document { get; private set; }

        public XmlElement DocumentElement
        {
            get { return Document.DocumentElement; }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructor

        private const string _rootName = "Root";
        private const string _nameSpace = @"http://www.w3.org";
        private const string _schemaInstance = @"http://www.w3.org/2001/XMLSchema-instance";


        public XmlWrapper()
        {
            XmlDocument document = new XmlDocument();
            XmlElement rootElement = document.CreateElement(_rootName);
            rootElement.SetAttribute("xmlns:xsd", _nameSpace);
            rootElement.SetAttribute("xmlns:xsi", _schemaInstance);

            document.AppendChild(rootElement);
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "utf-8", null);
            document.InsertBefore(declaration, rootElement);

            Document = document;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region XmlWrapper
        
        public void Clear()
        {
            Document[_rootName].RemoveAll();
        }
        
        public void Load(string filePath)
        {
            Document.Load(filePath);
        }

        private const string _dateTimeName = "DateTime";

        public void Save(string filePath)
        {
            XmlElement settingsElement = DocumentElement;
            XmlAttribute dateTimeAttribute = settingsElement.GetOrCreateAttribute(_dateTimeName);
            dateTimeAttribute.WriteDateTime(DateTime.Now);
            Document.Save(filePath);
        }


        #endregion
    }
}
