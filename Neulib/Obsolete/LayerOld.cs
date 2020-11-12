using System;
using System.Collections;
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
    public class Layer : BaseObject, IList<Neuron>
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// The neurons in this layer.
        /// </summary>
        private List<Neuron> Neurons { get; set; } = new List<Neuron>();

        public int Count { get => Neurons.Count; }

        public bool IsReadOnly => ((ICollection<Neuron>)Neurons).IsReadOnly;

        public Neuron this[int index] { get => Neurons[index]; set => Neurons[index] = value; }


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
        #region IList

        public int IndexOf(Neuron item)
        {
            return Neurons.IndexOf(item);
        }

        public void Insert(int index, Neuron item)
        {
            Neurons.Insert(index, item);
        }

        public void Add(Neuron item)
        {
            Neurons.Add(item);
        }

        public void RemoveAt(int index)
        {
            Neurons.RemoveAt(index);
        }

        public bool Remove(Neuron item)
        {
            return Neurons.Remove(item);
        }

        public void Clear()
        {
            Neurons.Clear();
        }

        public bool Contains(Neuron item)
        {
            return Neurons.Contains(item);
        }

        public void CopyTo(Neuron[] array, int arrayIndex)
        {
            Neurons.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Neuron> GetEnumerator()
        {
            return ((IEnumerable<Neuron>)Neurons).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Neurons).GetEnumerator();
        }


        public void ForEach(Action<Neuron> action, bool parallel = false)
        {
            int count = Neurons.Count;
            if (parallel)
                Parallel.For(0, count, j => action(Neurons[j]));
            else
                for (int j = 0; j < count; j++) action(Neurons[j]);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override string ToString()
        {
            return $"{Neurons.Count}";
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

        public void SetConnections(int connections)
        {
            int count = Neurons.Count;
            for (int i = 0; i < count; i++)
            {
                Neuron neuron = Neurons[i];
                neuron.Clear();
                for (int j = 0; j < connections; j++)
                {
                    neuron.Add(new Connection());
                }
            }
        }

        public void Randomize(Random random, float biasMagnitude, float weightMagnitude)
        {
            int count = Neurons.Count;
            for (int i = 0; i < count; i++)
                Neurons[i].Randomize(random, biasMagnitude, weightMagnitude);
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

        public void CalculateDeltas(Single1D ys, CostFunctionEnum costFunction)
        {
            if (Neurons.Count != ys.Count) throw new UnequalValueException(Neurons.Count, ys.Count, 426337);
            int count = Neurons.Count;
            ParallelFor(0, count, j => Neurons[j].CalculateDelta(ys[j], costFunction));
        }

        /// <summary> 
        /// Calculates the activation values of the neurons in this layer from the result values of the neurons in the previous layer, 
        /// and the bias and weight values of the neurons in this layer.
        /// </summary>
        public void FeedForward(Layer prevLayer)
        {
            int count = Neurons.Count;
            ParallelFor(0, count, j => Neurons[j].FeedForward(prevLayer));
        }

        public void FeedBackward(Layer nextLayer)
        // Backpropagate the error
        {
            if (nextLayer == null) throw new VarNullException(nameof(nextLayer), 384876);
            int count = Neurons.Count;
            ParallelFor(0, count, j => Neurons[j].FeedBackward(nextLayer, j));
        }

        public float SumWeightSqr(ref int n)
        {
            float sum = 0f;
            int count = Neurons.Count;
            for (int i = 0; i < count; i++)
            {
                sum  += Neurons[i].SumWeightSqr(ref n);
            }
            return sum;
        }

        #endregion
    }
}
