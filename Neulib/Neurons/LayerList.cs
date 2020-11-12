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
        private readonly List<Layer> _layers = new List<Layer>();

        /// <summary>
        /// The number of layers in this LayerList.
        /// </summary>
        public int Count { get => _layers != null ? _layers.Count : 0; }

        /// <summary>
        /// Gets or sets a layer.
        /// </summary>
        /// <param name="index">The index of the layer.</param>
        /// <returns>The layer referenced by index.</returns>
        public virtual Layer this[int index]
        {
            get { return _layers[index]; }
            set { _layers[index] = value; }
        }

        public bool IsReadOnly => ((ICollection<Layer>)_layers).IsReadOnly;

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
            return _layers.IndexOf(layer);
        }

        public virtual void Add(Layer layer)
        {
            if (layer == null) throw new VarNullException(nameof(layer), 424420);
            _layers.Add(layer);
        }

        public virtual void Insert(int index, Layer layer)
        {
            if (layer == null) throw new VarNullException(nameof(layer), 133650);
            _layers.Insert(index, layer);
        }

        public Layer Remove(int index)
        {
            Layer layer = _layers[index];
            _layers.RemoveAt(index);
            return layer;
        }

        public void RemoveAt(int index)
        {
            _layers.RemoveAt(index);
        }

        public bool Remove(Layer layer)
        {
            return _layers.Remove(layer);
        }

        public void Clear()
        {
            _layers.Clear();
        }

        public bool Contains(Layer item)
        {
            return _layers.Contains(item);
        }

        public void CopyTo(Layer[] array, int arrayIndex)
        {
            _layers.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Layer> GetEnumerator()
        {
            return ((IEnumerable<Layer>)_layers).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_layers).GetEnumerator();
        }

        public void ForEach(Action<Layer> action, bool parallel = false)
        {
            int count = _layers.Count;
            if (parallel)
                Parallel.For(0, count, j => action(_layers[j]));
            else
                for (int j = 0; j < count; j++) action(_layers[j]);
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
