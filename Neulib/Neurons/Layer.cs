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
    public class Layer : Unit
    {
        // Recursive, autonomous
        // ----------------------------------------------------------------------------------------
        #region Properties

        private Layer _previous;
        public Layer Previous
        {
            get { return _previous; }
            set
            {
                _previous = value;
                ClearConnections();
                AddConnections(value.OutputLayer);
            }
        }

        public Layer Next { get; set; }


        public virtual SingleLayer OutputLayer { get; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new autonomous layer.
        /// </summary>
        public Layer()
        {
        }

        /// <summary>
        /// Creates a new autonomous layer from the stream.
        /// </summary>
        public Layer(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Unit

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Layer

        public virtual void FeedForward(bool parallel)
        {
        }

        public virtual void CalculateDeltas(Single1D ys, CostFunctionEnum costFunction)
        // This must be the last layer in the network
        {
        }

        public virtual void FeedBackward(bool parallel)
        {
        }

        public void Insert(Layer layer)
        {
            layer.Previous = Previous;
            layer.Next = this;
            if (Previous != null) Previous.Next = layer;
            Previous = layer;
        }

        public void Remove()
        {
            if (Previous != null) Previous.Next = Next;
            if (Next != null) Next.Previous = Previous;
        }

        public void Replace(Layer layer)
        {
            if (Previous != null) Previous.Next = layer;
            layer.Previous = Previous;
            if (Next != null) Next.Previous = layer;
            layer.Next = Next;
        }

        #endregion
    }
}
