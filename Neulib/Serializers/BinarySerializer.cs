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

        private const ushort _NullToken = 0;

        public IBinarySerializable ReadValue(Stream stream)
        {
            CancellationTokenSource?.Token.ThrowIfCancellationRequested();
            ushort token = stream.ReadUshort();
            if (token == _NullToken)
                return null;
            Type type = Types.FirstOrDefault(pair => TypesDictionary.GetToken(pair.Key) == token).Value;
            if (type == null) // The stream contains an undefined token
                throw new InvalidValueException($"The stream contains an unknown token: {token}", 944327);
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
                stream.WriteUshort(_NullToken);
                return;
            }
            Type type = serializable.GetType();
            string name = Types.FirstOrDefault(pair => pair.Value == type).Key;
            if (string.IsNullOrEmpty(name))
                throw new InvalidValueException($"Type '{serializable.GetType()}' is not registered in TypesDictionary", 468077);
            ushort token = TypesDictionary.GetToken(name);
            stream.WriteUshort(token);
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
            long pos = stream.Position;
            try
            {
                Version = Version.Parse(stream.ReadString());
            }
            catch
            {
                Version = new Version(3, 2, 8, 0); // The first published version which didn't have this Version feature.
                stream.Position = pos;
            }
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

