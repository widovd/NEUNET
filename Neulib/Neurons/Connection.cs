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
    /// </summary>
    public class Connection : BaseObject
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// The weight value of the connection.
        /// </summary>
        public float Weight { get; set; } = 1f;

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
        public Connection(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
            Weight = stream.ReadSingle();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override string ToString()
        {
            return $"weight = {Weight:F3}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Connection value = o as Connection ?? throw new InvalidTypeException(o, nameof(Connection), 759981);
            Weight = value.Weight;
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
            stream.WriteSingle(Weight);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Connection

        #endregion
    }
}
