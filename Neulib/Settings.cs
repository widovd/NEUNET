using System;
using System.Xml;
using System.ComponentModel;
using System.Runtime.Serialization;
using Neulib.Exceptions;
using Neulib.Serializers;

namespace Neulib
{

    public class Settings : BaseObject
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Settings()
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Settings value = o as Settings ?? throw new InvalidTypeException(o, nameof(Settings), 458027);
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region CalculationSettings

        public virtual void LoadFromSettings(XmlElement rootElement)
        {
        }

        public virtual void SaveToSettings(XmlElement rootElement)
        {
        }

        public Settings DefaultSettings()
        {
            return (Settings)CreateNew();
        }

        #endregion
    }
}
