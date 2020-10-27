using System.Xml;
using Neulib.Serializers;

namespace Neunet.Serializers
{

    public class XmlSettings: XmlWrapper
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private const string _formsId = "Forms";

        public XmlElement FormsElement
        {
            get { return DocumentElement.GetOrCreateElement(_formsId); }
        }

        private const string _globalsId = "Globals";

        public XmlElement GlobalsElement
        {
            get { return DocumentElement.GetOrCreateElement(_globalsId); }
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
