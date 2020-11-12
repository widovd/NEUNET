using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Numerics;
using Neulib.Serializers;
using static Neulib.Extensions.FloatExtensions;
using System.Collections;
using static System.Math;

namespace Neulib.Neurons
{
    public class LayerList : Unit, IList<Layer>
    {
        // ----------------------------------------------------------------------------------------
        #region LayerList properties

        /// <summary>
        /// The layers of this LayerList.
        /// </summary>
        private List<Layer> Layers { get; set; } = new List<Layer>();

        /// <summary>
        /// The number of layers in this LayerList.
        /// </summary>
        public int Count { get => Layers.Count; }

        /// <summary>
        /// Gets or sets a layer.
        /// </summary>
        /// <param name="index">The index of the layer.</param>
        /// <returns>The layer referenced by index.</returns>
        public Layer this[int index]
        {
            get { return Layers[index]; }
            set { Layers[index] = value; }
        }

        public bool IsReadOnly => ((ICollection<Layer>)Layers).IsReadOnly;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new instance of LayerList.
        /// </summary>
        public LayerList()
        {
        }

        /// <summary>
        /// Creates a new LayerList from the stream.
        /// </summary>
        public LayerList(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
            int count = stream.ReadInt();
            for (int i = 0; i < count; i++)
            {
                Add((Layer)stream.ReadValue(serializer));
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IList

        public int IndexOf(Layer layer)
        {
            return Layers.IndexOf(layer);
        }

        public void Add(Layer layer)
        {
            Layers.Add(layer);
        }

        public void Insert(int index, Layer layer)
        {
            Layers.Insert(index, layer);
        }

        public Layer Remove(int index)
        {
            Layer layer = Layers[index];
            Layers.RemoveAt(index);
            return layer;
        }

        public void RemoveAt(int index)
        {
            Remove(index);
        }

        public bool Remove(Layer layer)
        {
            return Layers.Remove(layer);
        }

        public void Clear()
        {
            Layers.Clear();
        }

        public bool Contains(Layer item)
        {
            return Layers.Contains(item);
        }

        public void CopyTo(Layer[] array, int arrayIndex)
        {
            Layers.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Layer> GetEnumerator()
        {
            return ((IEnumerable<Layer>)Layers).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Layers).GetEnumerator();
        }

        public void ForEach(Action<Layer> action, bool parallel = false)
        {
            int count = Layers.Count;
            if (parallel)
                Parallel.For(0, count, j => action(Layers[j]));
            else
                for (int j = 0; j < count; j++) action(Layers[j]);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            LayerList value = o as LayerList ?? throw new InvalidTypeException(o, nameof(LayerList), 321155);
            Clear();
            value.ForEach(layer => Add((Layer)layer.Clone()));
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
            stream.WriteInt(Count);
            ForEach(layer => stream.WriteValue(layer, serializer));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Unit

        public override int CoefficientCount()
        {
            int count = 0;
            ForEach(layer => count += layer.CoefficientCount());
            return count;
        }

        public override int SetCoefficients(Single1D coefficients, int index)
        {
            ForEach(layer => index = layer.SetCoefficients(coefficients, index));
            return index;
        }

        public override int GetCoefficients(Single1D coefficients, int index)
        {
            ForEach(layer => index = layer.GetCoefficients(coefficients, index));
            return index;
        }

        public override int AddDerivatives(Single1D derivatives, int index, float lambdaDivN)
        {
            ForEach(layer => index = layer.AddDerivatives(derivatives, index, lambdaDivN));
            return index;
        }

        public override void Randomize(Random random, float biasMagnitude, float weightMagnitude)
        {
            ForEach(layer => layer.Randomize(random, biasMagnitude, weightMagnitude));
        }

        public override int CountWeight()
        {
            int count = 0;
            ForEach(layer => count += layer.CountWeight());
            return count;
        }

        public override float SumWeightSqr()
        {
            float sum = 0f;
            ForEach(layer => sum += layer.SumWeightSqr());
            return sum;
        }

        public override int SetActivations(Single1D activations, int index)
        {
            ForEach(layer => index = layer.SetActivations(activations, index));
            return index;
        }

        public override int GetActivations(Single1D activations, int index)
        {
            ForEach(layer => index = layer.GetActivations(activations, index));
            return index;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region LayerList

        #endregion
    }
}
