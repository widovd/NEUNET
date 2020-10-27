using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Numerics;
using static Neulib.Extensions.FloatExtensions;
using static System.Math;

namespace Neulib.NeuronsNew
{
    /// <summary>
    /// Represents a layer of neurons in a neural network.
    /// </summary>
    public class LayerData
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public LayerStruc LayerStruc { get; set; }
        ///// <summary>
        ///// The neural network which contains this layer.
        ///// </summary>
        //public NetworkData Parent { get; set; }

        ///// <summary>
        ///// The previous layer, or null if this is the first layer.
        ///// </summary>
        //public LayerData Previous { get; set; }

        ///// <summary>
        ///// The next layer, or null if this is the last layer.
        ///// </summary>
        //public LayerData Next { get; set; }

        ///// <summary>
        ///// returns the number of neurons in this layer.
        ///// </summary>
        //public int Count { get => _neurons != null ? _neurons.Length : 0; }

        //protected readonly NeuronData[] _neurons;

        ///// <summary>
        ///// Returns the j-th neuron in this layer.
        ///// </summary>
        ///// <param name="j">The index of the neuron in this layer.</param>
        ///// <returns>The neuron.</returns>
        //public NeuronData this[int j]
        //{
        //    get => _neurons[j];
        //}

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new layer of neurons.
        /// </summary>
        /// <param name="count">The number of neurons in this layer.</param>
        public LayerData(LayerStruc layerStruc)
        {
            LayerStruc = layerStruc;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Layer

        public void SetActivations(float[] xs)
        {
            if (LayerStruc.Count != xs.Length) throw new UnequalValueException(LayerStruc.Count, xs.Length, 480461);
            LayerStruc.ForEach((neuron, j) => neuron.Activation = xs[j]);
        }

        public void GetActivations(float[] ys)
        {
            if (LayerStruc.Count != ys.Length) throw new UnequalValueException(LayerStruc.Count, ys.Length, 953472);
            LayerStruc.ForEach((neuron, j) => ys[j] = neuron.Activation);
        }

        public void CalculateDeltas(float[] ys)
        {
            if (LayerStruc.Count != ys.Length) throw new UnequalValueException(LayerStruc.Count, ys.Length, 426337);
            LayerStruc.ForEach((neuron, j) => neuron.CalculateDelta(ys[j]));
        }


        /// <summary>
        /// Fills a float array with the biasses and weights of all neurons in this layer. The float array must have been created.
        /// </summary>
        /// <param name="p">The float array.</param>
        public void GetCoefficients(float[] p)
        {
            int h = 0;
            ForEach(neuron =>
            {
                p[h++] = neuron.Bias;
                neuron.ForEach(connection => { p[h++] = connection.Weight; });
            });
        }

        /// <summary>
        /// Updates the biasses and weights of all neurons in this layer with the values of the float array.
        /// </summary>
        /// <param name="p">The float array.</param>
        public void SetCoefficients(float[] p)
        {
            int h = 0;
            ForEach((NeuronData neuron) =>
            {
                neuron.Bias = p[h++];
                neuron.ForEach(connection => { connection.Weight = p[h++]; });
            });
        }

        /// <summary> 
        /// Calculates the activation values of the neurons in this layer from the result values of the neurons in the previous layer, 
        /// and the bias and weight values of the neurons in this layer.
        /// </summary>
        public void Calculate()
        {
            ForEach(neuron => neuron.FeedForward(), true);
        }

        public void FeedBackward()
        // Backpropagate the error
        {
            LayerData nextLayer = Next;
            if (nextLayer == null) return;
            ForEach((neuron, i) =>
            {
                float delta = 0f;
                nextLayer.ForEach(nextNeuron =>
                {
                    delta += nextNeuron[i].Weight * nextNeuron.Delta; // wji
                });
                delta *= NeuronData.ActivationDerivative(neuron.Activation);
                neuron.Delta = delta;
            }, true);
        }


        #endregion
    }
}
