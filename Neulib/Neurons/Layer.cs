using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Serializers;
using Neulib.Numerics;
using static Neulib.Extensions.FloatExtensions;
using static System.Math;

namespace Neulib.Neurons
{
    /// <summary>
    /// Represents a layer of neurons in a neural network.
    /// </summary>
    public class Layer : BaseObject
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// The neurons in this layer.
        /// </summary>
        public List<Neuron> Neurons { get; private set; } = new List<Neuron>();

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new layer.
        /// </summary>
        public Layer()
        {
        }

        /// <summary>
        /// Creates a new layer from the stream.
        /// </summary>
        public Layer(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
            int count = stream.ReadInt();
            for (int i = 0; i < count; i++)
            {
                Neurons.Add((Neuron)stream.ReadValue(serializer));
            }
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Layer value = o as Layer ?? throw new InvalidTypeException(o, nameof(Layer), 419962);
            int count = value.Neurons.Count;
            Neurons.Clear();
            for (int i = 0; i < count; i++)
            {
                Neurons.Add((Neuron)value.Neurons[i].Clone());
            }
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
            int count = Neurons.Count;
            stream.WriteInt(count);
            for (int i = 0; i < count; i++)
            {
                stream.WriteValue(Neurons[i], serializer);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Layer

        public void ForEach(Action<Neuron> action, bool parallel = false)
        {
            int count = Neurons.Count;
            if (parallel)
                Parallel.For(0, count, j => action(Neurons[j]));
            else
                for (int j = 0; j < count; j++) action(Neurons[j]);
        }

        public void ForEach(Action<Neuron, int> action, bool parallel = false)
        {
            int count = Neurons.Count;
            if (parallel)
                Parallel.For(0, count, j => action(Neurons[j], j));
            else
                for (int j = 0; j < count; j++) action(Neurons[j], j);
        }

        public void InitializeNeurons(int count, Layer previous, Random random, float biasMagnitude, float weightMagnitude)
        {
            for (int j = 0; j < count; j++)
            {
                Neuron neuron = new Neuron(random, biasMagnitude);
                Neurons.Add(neuron);
                if (previous == null) continue;
                for (int i = 0; i < previous.Neurons.Count; i++)
                {
                    neuron.Connections.Add(new Connection(random, weightMagnitude));
                }
            }
        }

        public void SetActivations(Single1D xs)
        {
            if (Neurons.Count != xs.Count) throw new UnequalValueException(Neurons.Count, xs.Count, 480461);
            int count = Neurons.Count;
            ParallelFor(0, count, j => Neurons[j].Activation = xs[j]);
        }

        public void GetActivations(Single1D output)
        {
            if (Neurons.Count != output.Count) throw new UnequalValueException(Neurons.Count, output.Count, 953472);
            int count = Neurons.Count;
            ParallelFor(0, count, j => output[j] = Neurons[j].Activation);
        }

        public void CalculateDeltas(Single1D ys)
        {
            if (Neurons.Count != ys.Count) throw new UnequalValueException(Neurons.Count, ys.Count, 426337);
            int count = Neurons.Count;
            ParallelFor(0, count, j => Neurons[j].CalculateDelta(ys[j]));
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
        public void Calculate(Layer prevLayer)
        {
            int count = Neurons.Count;
            ParallelFor(0, count, j => Neurons[j].FeedForward(prevLayer));
        }

        public void FeedBackward(Layer nextLayer)
        // Backpropagate the error
        {
            if (nextLayer == null) throw new VarNullException(nameof(nextLayer), 384876);
            int count = Neurons.Count;
            ParallelFor(0, count, j =>
            {
                Neuron neuron = Neurons[j];
                float delta = 0f;
                int nextCount = nextLayer.Neurons.Count;
                for (int k = 0; k < nextCount; k++)
                {
                    Neuron nextNeuron = nextLayer.Neurons[k];
                    delta += nextNeuron.Connections[j].Weight * nextNeuron.Delta; // wji
                }
                delta *= Neuron.ActivationDerivative(neuron.Activation);
                neuron.Delta = delta;
            });
        }


        #endregion
    }
}
