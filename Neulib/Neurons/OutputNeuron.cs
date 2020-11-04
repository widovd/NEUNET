﻿using System;
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
    /// Represents the output neuron for use in the last layer of a neural network.
    /// </summary>
    public class OutputNeuron : HiddenNeuron
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new input neuron.
        /// </summary>
        public OutputNeuron()
        {
        }

        /// <summary>
        /// Creates a new input neuron from the stream.
        /// </summary>
        public OutputNeuron(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override string ToString()
        {
            return base.ToString();
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
            base.Randomize(random, biasMagnitude, weightMagnitude);
        }

        /// <summary> 
        /// Calculates the activation value of this neuron from the previous layer.
        /// </summary>
        public override void FeedForward(Layer prevLayer)
        {
            base.FeedForward(prevLayer);
        }

        /// <summary> 
        /// Calculates the error or delta value of this neuron from the next layer.
        /// </summary>
        public override void FeedBackward(Layer nextLayer, int j)
        {
            base.FeedBackward(nextLayer, j);
        }

        /// <summary>
        /// Calculates Delta of this neuron which must be in the last layer.
        /// </summary>
        /// <param name="y">The required activation value.</param>
        /// <param name="costFunction">Defines the cost function.</param>
        public override void CalculateDelta(float y, CostFunctionEnum costFunction)
        {
            base.CalculateDelta(y, costFunction);
        }

        #endregion
    }
}
