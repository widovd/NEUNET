using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Serializers;
using Neulib.Numerics;

namespace Neulib.Neurons
{
    /// <summary>
    /// Represents a connection between two neurons.
    /// This class is not derived from Unit because it does not contain a neuron.
    /// </summary>
    public class Connection : BaseObject
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// The index of the source neuron in the previous layer.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The weight value of the connection.
        /// </summary>
        public float Weight { get; set; } = float.NaN;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new connection.
        /// </summary>
        public Connection()
        {
        }

        /// <summary>
        /// Creates a new connection from the stream.
        /// </summary>
        public Connection(Stream stream, Serializer serializer) : base(stream, serializer)
        {
            Index = stream.ReadInt();
            Weight = stream.ReadSingle();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override string ToString()
        {
            return $"index = {Index}, weight = {Weight:F3}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Connection value = o as Connection ?? throw new InvalidTypeException(o, nameof(Connection), 759981);
            Index = value.Index;
            Weight = value.Weight;
        }

        public override void WriteToStream(Stream stream, Serializer serializer)
        {
            base.WriteToStream(stream, serializer);
            stream.WriteInt(Index);
            stream.WriteSingle(Weight);
        }

        #endregion
    }
}
