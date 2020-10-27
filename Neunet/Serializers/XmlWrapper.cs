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

        protected virtual string DocumentFileName
        {
            get { return "Fresh3.xml"; }
        }

        protected string DocumentFilePath
        {
            get { return Path.Combine(Program.ApplicationData, DocumentFileName); }
        }

        public XmlElement DocumentElement
        {
            get { return Document.DocumentElement; }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructor

        private const string _rootName = "Root";
        private const string _nameSpace = @"http://www.signify.com/home";
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

        public void Load()
        {
            string filePath = Path.Combine(Program.ApplicationData, DocumentFileName);
            if (File.Exists(filePath)) { Load(filePath); return; }
            filePath = Path.Combine(Program.CommonApplicationData, DocumentFileName);
            if (File.Exists(filePath)) { Load(filePath); return; }
        }

        public void Save(string filePath)
        {
            Document.Save(filePath);
        }

        private const string _dateTimeName = "DateTime";
        public void Save()
        {
            XmlElement settingsElement = DocumentElement;
            XmlAttribute dateTimeAttribute = settingsElement.GetOrCreateAttribute(_dateTimeName);
            dateTimeAttribute.WriteDateTime(DateTime.Now);
            string fileFolder = Program.ApplicationData;
            string fileName = DocumentFileName;
            string filePath = Path.Combine(fileFolder, fileName);
            if (!Directory.Exists(fileFolder))
                Directory.CreateDirectory(fileFolder);
            else if (File.Exists(filePath))
            {
                string previousPath = Path.Combine(fileFolder, Path.GetFileNameWithoutExtension(fileName) + "_old.xml");
                if (File.Exists(previousPath))
                    File.Delete(previousPath);
                File.Move(filePath, previousPath);
            }
            Save(filePath);
        }

        #endregion
    }
}
