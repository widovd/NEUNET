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

        public int SampleCount { get; set; } = 100;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public CalculationSettings()
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region CalculationSettings

        private const string _MaxIterId = "MaxIter";

        public void LoadFromSettings(XmlElement rootElement)
        {
            MaxIter = rootElement.ReadInt(_MaxIterId, MaxIter);
        }

        public void SaveToSettings(XmlElement rootElement)
        {
            rootElement.WriteInt(_MaxIterId, MaxIter);
        }

        public void Default()
        {

        }

        #endregion
    }
}
