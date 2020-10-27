using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Exceptions;
using static System.Math;

namespace Neulib.NeuronsNew
{
    /// <summary>
    /// Represents a neuron for use in a neural network.
    /// </summary>
    public class NeuronData
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public NeuronStruc Neuron { get; set; }

        /// <summary>
        /// The weighted sum value of this neuron.
        /// </summary>
        public float Sum { get; private set; } = 0f;

        /// <summary>
        /// The activation value of this neuron.
        /// </summary>
        public float Activation { get; set; } = 0f;

        /// <summary>
        /// The delta error value of this neuron.
        /// </summary>
        public float Delta { get; set; } = 0f;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public NeuronData(NeuronStruc neuron)
        {
            Neuron = neuron;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override string ToString()
        {
            return $"sum = {Sum:F3}, activation = {Activation:F3}, delta = {Delta:F3}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Neuron

        public static float ActivationFunction(float z)
        {
            return 1f / (1f + (float)Exp(-z));
        }

        public static float ActivationDerivative(float a)
        // a is the activation value
        {
            return a * (1f - a);
        }

        /// <summary> 
        /// Calculates the activation value of this neuron from the neurons in the previous layer, the bias, and the activation function.
        /// </summary>
        public void FeedForward()
        {
            float sum = Neuron.Bias;
            Neuron.ForEach(connection => sum += connection.Product);
            Sum = sum;
            Activation = ActivationFunction(sum);
        }


        public void CalculateDelta(float y)
        {
            Delta = (Activation - y) * ActivationDerivative(Activation);
        }

        #endregion
    }
}
