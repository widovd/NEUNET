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

        public virtual object Deserialize(Stream stream)
        {
            return null;
        }

        public virtual void Serialize(Stream stream, object graph)
        {
        }

        #endregion
    }
}
