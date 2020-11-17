using System;
using System.Linq;
using System.IO;
using System.Threading;
using Neulib.Exceptions;

namespace Neulib.Serializers
{
    public sealed class BinarySerializer : Serializer
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public BinarySerializer(CancellationTokenSource tokenSource) : base(tokenSource)
        { }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Serializer, IFormatter

        private const int _nullToken = 0;

        public IBinarySerializable ReadValue(Stream stream)
        {
            CancellationTokenSource?.Token.ThrowIfCancellationRequested();
            int token = stream.ReadInt();
            if (token == _nullToken)
                return null;
            Type type = Types.GetType(token);
            long pos2 = stream.ReadLong();
            IBinarySerializable serializable = null;
            try
            {
                serializable = Activator.CreateInstance(type, stream, this) as IBinarySerializable;
            }
            catch (MissingMethodException ex)
            {
                throw new InvalidCodeException($"Activator.CreateInstance({type}) failed.", ex, 563178);
            }
            catch
            {
                // the serializable is not properly read from stream, but the ctor is called with default properties
            }
            stream.Position = pos2;
            return serializable;
        }

        public void WriteValue(Stream stream, IBinarySerializable serializable)
        {
            CancellationTokenSource?.Token.ThrowIfCancellationRequested();
            if (serializable == null)
            {
                stream.WriteInt(_nullToken);
                return;
            }
            int token = Types.GetToken(serializable.GetType());
            stream.WriteInt(token);
            long pos1 = stream.Position;
            stream.WriteLong(0);
            serializable.WriteToStream(stream, this);
            long pos2 = stream.Position;
            stream.Position = pos1;
            stream.WriteLong(pos2);
            stream.Position = pos2;
        }

        public override object Deserialize(Stream stream)
        {
            Version = Version.Parse(stream.ReadString());
            return ReadValue(stream);
        }

        public override void Serialize(Stream stream, object graph)
        {
            if (!(graph is IBinarySerializable serializable)) return;
            stream.WriteString(Version.ToString());
            WriteValue(stream, serializable);
        }

        #endregion
    }
}

