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
    public class SingleLayer : Layer, IList<Neuron>
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

        public override SingleLayer OutputLayer { get => this; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new layer.
        /// </summary>
        public SingleLayer()
        {
        }

        /// <summary>
        /// Creates a new layer with count sigmoid neurons.
        /// </summary>
        /// <param name="count">The required number of sigmoid neurons.</param>
        public SingleLayer(int count)
        {
            for (int i = 0; i < count; i++)
                Add(new Sigmoid());
        }

        /// <summary>
        /// Creates a new layer from the stream.
        /// </summary>
        public SingleLayer(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
            int count = stream.ReadInt();
            for (int i = 0; i < count; i++)
            {
                Add((Neuron)stream.ReadValue(serializer));
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
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            SingleLayer value = o as SingleLayer ?? throw new InvalidTypeException(o, nameof(SingleLayer), 854053);
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
        #region Unit

        public override int CoefficientCount()
        {
            int count = 0;
            ForEach(neuron => count += neuron.CoefficientCount());
            return count;
        }

        public override int SetCoefficients(Single1D coefficients, int index)
        {
            ForEach(neuron => index = neuron.SetCoefficients(coefficients, index));
            return index;
        }

        public override int GetCoefficients(Single1D coefficients, int index)
        {
            ForEach(neuron => index = neuron.GetCoefficients(coefficients, index));
            return index;
        }

        public override int AddDerivatives(Single1D derivatives, int index, float lambdaDivN)
        {
            ForEach(neuron => index = neuron.AddDerivatives(derivatives, index, lambdaDivN));
            return index;
        }

        public override void Randomize(Random random, float biasMagnitude, float weightMagnitude)
        {
            ForEach(neuron => neuron.Randomize(random, biasMagnitude, weightMagnitude));
        }

        public override int CountWeight()
        {
            int count = 0;
            ForEach(neuron => count += neuron.CountWeight());
            return count;
        }

        public override float SumWeightSqr()
        {
            float sum = 0f;
            ForEach(neuron => sum += neuron.SumWeightSqr());
            return sum;
        }

        public override int SetActivations(Single1D activations, int index)
        {
            ForEach(neuron => index = neuron.SetActivations(activations, index));
            return index;
        }

        public override int GetActivations(Single1D activations, int index)
        {
            ForEach(neuron => index = neuron.GetActivations(activations, index));
            return index;
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Layer


        public override void ClearConnections()
        {
            ForEach(neuron => neuron.ClearConnections());
        }

        public override void AddConnections(SingleLayer prevLayer)
        {
            ForEach(neuron => neuron.AddConnections(prevLayer));
        }

        public override void FeedForward(bool parallel)
        {
            ParallelFor(0, Neurons.Count, j => Neurons[j].FeedForward(), parallel);
        }

        public override void CalculateDeltas(Single1D ys, CostFunctionEnum costFunction)
        // This must be the last layer in the network
        {
            int j = 0;
            ForEach(neuron => neuron.CalculateDelta(ys[j++], costFunction));
        }

        public override void FeedBackward(bool parallel)
        {
            int count = Neurons.Count;
            // ToDo: dit gaat niet goed wanneer de vorige laag geen SingleLayer is
            SingleLayer previous = Previous as SingleLayer ??
                throw new VarNullException(nameof(previous), 409108);
            ParallelFor(0, count, j => previous[j].FeedBackward(this, j));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region SingleLayer

        public float SumWeightDelta(int j)
        {
            float d = 0f;
            int count = Count;
            for (int k = 0; k < count; k++)
            {
                Neuron neuron = Neurons[k];
                d += neuron[j].Weight * neuron.Delta; // wji
            }
            return d;
        }


        #endregion
    }
}
