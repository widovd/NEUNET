using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Neulib.Serializers
{
    public class Serializer
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public TypesDictionary Types { get; private set; } = new TypesDictionary();

        public CancellationTokenSource CancellationTokenSource { get; private set; } = null;

        public Version Version { get; set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Serializer(CancellationTokenSource tokenSource)
        {
            CancellationTokenSource = tokenSource;
            Version = Assembly.GetExecutingAssembly().GetName().Version;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Serializer, IFormatter

        public virtual ISerializable ReadValue(Stream stream)
        {
            return null;
        }

        public virtual void WriteValue(Stream stream, ISerializable serializable)
        {
        }

        public virtual ISerializable Deserialize(Stream stream)
        {
            return null;
        }

        public virtual void Serialize(Stream stream, ISerializable serializable)
        {
        }

        #endregion
    }
}
