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
        /// The neuron in the previous layer which is the source of the activation.
        /// </summary>
        public Neuron Neuron { get; set; }

        /// <summary>
        /// The weight value of the connection.
        /// </summary>
        public float Weight { get; set; } = 1f;

        public float Activation
        {
            get => Neuron != null ? Weight * Neuron.Activation : throw new VarNullException(nameof(Neuron), 367776);
        }
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
        /// <remarks>Neuron can not be read from stream. Must be set by the parent network.</remarks>
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
        #region Unit


        #endregion
    }
}
