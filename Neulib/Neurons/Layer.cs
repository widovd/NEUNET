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
    public class Layer : BaseObject
    {
        // Recursive, autonomous
        // ----------------------------------------------------------------------------------------
        #region Properties

        private Layer _previous;
        public virtual Layer Previous
        {
            get { return _previous; }
            set
            {
                _previous = value;
                SetConnections(value);
            }
        }

        private Layer _next;
        public virtual Layer Next
        {
            get { return _next; }
            set
            {
                _next = value;
                value.SetConnections(this);
            }
        }

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
        #region AutonomousLayer

        public virtual float SumWeightDeltaFirstLayer(int j)
        {
            return 0f;
        }

        public virtual float GetActivationLastLayer(int i)
        {
            return 0f;
        }

        public virtual void GetActivationsLastLayer(Single1D output)
        {
        }


        public virtual void SetActivationsFirstLayer(Single1D xs)
        {
        }

        public virtual void CalculateDeltasLastLayer(Single1D ys, CostFunctionEnum costFunction)
        {
        }


        public virtual void SetConnections(Layer layer)
        {
        }

        public virtual void Randomize(Random random, float biasMagnitude, float weightMagnitude)
        {
        }

        public virtual void FeedForward()
        {
        }

        public virtual void FeedBackward()
        {
        }

        #endregion
    }
}
