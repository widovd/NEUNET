using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Extensions;
using Neulib.Serializers;
using static System.Math;

namespace Neulib.Neurons
{
    /// <summary>
    /// Represents the input neuron for use in the first layer of a neural network.
    /// </summary>
    public class InputNeuron : Neuron
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new input neuron.
        /// </summary>
        public InputNeuron()
        {
        }

        /// <summary>
        /// Creates a new input neuron from the stream.
        /// </summary>
        public InputNeuron(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override string ToString()
        {
            return $"activation = {Activation:F3}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region InputNeuron

        public override void Randomize(Random random, float biasMagnitude, float weightMagnitude)
        {
            throw new InvalidCallException(nameof(Randomize), 937762);
        }

        /// <summary> 
        /// Calculates the activation value of this neuron from the previous layer.
        /// </summary>
        public override void FeedForward(Layer prevLayer)
        {
            throw new InvalidCallException(nameof(FeedForward), 569595);
        }

        /// <summary> 
        /// Calculates the error or delta value of this neuron from the next layer.
        /// </summary>
        public override void FeedBackward(Layer nextLayer, int j)
        {
            throw new InvalidCallException(nameof(FeedBackward), 806703);
        }

        /// <summary>
        /// Calculates Delta of this neuron which must be in the last layer.
        /// </summary>
        /// <param name="y">The required activation value.</param>
        /// <param name="costFunction">Defines the cost function.</param>
        public override void CalculateDelta(float y, CostFunctionEnum costFunction)
        {
            throw new InvalidCallException(nameof(CalculateDelta), 827839);
        }

        #endregion
    }
}
