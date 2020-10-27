using System.Xml;
using Neulib.Serializers;

namespace Neunet.Serializers
{

    public class XmlSettings: XmlWrapper
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        protected override string DocumentFileName
        {
            get { return "Settings.xml"; }
        }

        private const string _formsId = "FormList";

        public XmlElement FormsElement
        {
            get { return DocumentElement.GetOrCreateElement(_formsId); }
        }

        private const string _globalsId = "Globals";

        public XmlElement GlobalsElement
        {
            get { return DocumentElement.GetOrCreateElement(_globalsId); }
        }

        private const string _testId = "Test";

        public XmlElement TestElement
        {
            get { return DocumentElement.GetOrCreateElement(_testId); }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructor

        public XmlSettings()
        {
        }

        #endregion
    }

}
