using System;
using System.Xml;
using Neulib.Serializers;

namespace Neulib
{
    public class CalculationSettings : BaseObject
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public int MaxIter { get; set; } = 100;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public CalculationSettings(bool load = false)
        {
            if (load) LoadFromSettings();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region CalculationSettings

        private const string _CalculationSettingsId = "CalculationSettings";

        public void LoadFromSettings()
        {
            //XmlElement globalsElement = Program.GlobalsElement;
            //if (globalsElement == null) return;
            //XmlElement rootElement = globalsElement.GetOrCreateElement(_CalculationSettingsId);
        }

        public void SaveToSettings()
        {
            //XmlElement globalsElement = Program.GlobalsElement;
            //if (globalsElement == null) return;
            //XmlElement rootElement = globalsElement.GetOrCreateElement(_CalculationSettingsId);
        }

        #endregion
    }
}
