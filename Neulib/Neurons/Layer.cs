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
                SetConnections(value);
            }
        }

        private Layer _next;
        public Layer Next
        {
            get { return _next; }
            set
            {
                _next = value;
                //value.SetConnections(this);
            }
        }

        public virtual int InputCount
        {
            get; 
        }

        public virtual int OutputCount
        {
            get;
        }

        public virtual SingleLayer FirstSingleLayer { get; }

        public virtual SingleLayer LastSingleLayer { get; }

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

        public virtual float SumWeightDeltaFirstLayer(int j)
        {
            return 0f;
        }

        public virtual float GetActivationLastLayer(int i)
        {
            return 0f;
        }


        public virtual void FeedForward()
        {
        }

        public virtual void FeedBackward()
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
