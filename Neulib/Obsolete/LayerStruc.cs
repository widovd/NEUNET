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
    public class LayerStruc
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// The neural network which contains this layer.
        /// </summary>
        public NetworkStruc Parent { get; set; }

        /// <summary>
        /// The previous layer, or null if this is the first layer.
        /// </summary>
        public LayerStruc Previous { get; set; }

        /// <summary>
        /// The next layer, or null if this is the last layer.
        /// </summary>
        public LayerStruc Next { get; set; }

        /// <summary>
        /// returns the number of neurons in this layer.
        /// </summary>
        public int Count { get => _neurons != null ? _neurons.Length : 0; }

        protected readonly NeuronStruc[] _neurons;

        /// <summary>
        /// Returns the j-th neuron in this layer.
        /// </summary>
        /// <param name="j">The index of the neuron in this layer.</param>
        /// <returns>The neuron.</returns>
        public NeuronStruc this[int j]
        {
            get => _neurons[j];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new layer of neurons.
        /// </summary>
        /// <param name="count">The number of neurons in this layer.</param>
        public LayerStruc(int count)
        {
            _neurons = new NeuronStruc[count];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Layer

        public void ForEach(Action<NeuronStruc> action, bool parallel = false)
        {
            int count = Count;
            if (parallel)
                Parallel.For(0, count, j => action(_neurons[j]));
            else
                for (int j = 0; j < count; j++) action(_neurons[j]);
        }

        public void ForEach(Action<NeuronStruc, int> action, bool parallel = false)
        {
            int count = Count;
            if (parallel)
                Parallel.For(0, count, j => action(_neurons[j], j));
            else
                for (int j = 0; j < count; j++) action(_neurons[j], j);
        }

        public void InitializeNeurons()
        {
            LayerStruc previous = Previous;
            int count = Count;
            for (int j = 0; j < count; j++)
                _neurons[j] = new NeuronStruc(previous);
        }

        public void SetActivations(float[] xs)
        {
            if (Count != xs.Length) throw new UnequalValueException(Count, xs.Length, 480461);
            ForEach((neuron, j) => neuron.Activation = xs[j]);
        }

        public void GetActivations(float[] ys)
        {
            if (Count != ys.Length) throw new UnequalValueException(Count, ys.Length, 953472);
            ForEach((neuron, j) => ys[j] = neuron.Activation);
        }

        public void CalculateDeltas(float[] ys)
        {
            if (Count != ys.Length) throw new UnequalValueException(Count, ys.Length, 426337);
            ForEach((neuron, j) => neuron.CalculateDelta(ys[j]));
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
            ForEach((Neuron neuron) =>
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
            Layer nextLayer = Next;
            if (nextLayer == null) return;
            ForEach((neuron, i) =>
            {
                float delta = 0f;
                nextLayer.ForEach(nextNeuron =>
                {
                    delta += nextNeuron[i].Weight * nextNeuron.Delta; // wji
                });
                delta *= Neuron.ActivationDerivative(neuron.Activation);
                neuron.Delta = delta;
            }, true);
        }


        #endregion
    }
}
