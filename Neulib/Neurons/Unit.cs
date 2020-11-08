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
    public class Unit : BaseObject
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new unit.
        /// </summary>
        public Unit()
        {
        }

        /// <summary>
        /// Creates a new unit from the stream.
        /// </summary>
        public Unit(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Unit

        public virtual void Randomize(Random random, float biasMagnitude, float weightMagnitude)
        {
        }

        /// <summary>
        /// Returns the total number of biasses and weights of all neurons in this network.
        /// </summary>
        public virtual int CoefficientCount()
        {
            return 0;
        }

        public Single1D CreateCoefficients()
        {
            return new Single1D(CoefficientCount());
        }

        /// <summary>
        /// Sets the biasses and weights of all neurons in this layer.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        public virtual int SetCoefficients(Single1D coefficients, int index)
        {
            return index;
        }

        /// <summary>
        /// Gets the biasses and weights of all neurons in this layer.
        /// </summary>
        /// <param name="coefficients">The coefficients.</param>
        public virtual int GetCoefficients(Single1D coefficients, int index)
        {
            return index;
        }

        public Single1D GetCoefficients()
        {
            Single1D coefficients = CreateCoefficients();
            GetCoefficients(coefficients, 0);
            return coefficients;
        }

        public virtual int AddDerivatives(Single1D derivatives, int index, float lambdaDivN)
        {
            return index;
        }

        public virtual int CountWeight()
        {
            return 0;
        }

        public virtual float SumWeightSqr()
        {
            return 0f;
        }

        public virtual void SetConnections(Layer prevLayer)
        {
        }

        public virtual int SetActivations(Single1D activations, int index)
        {
            return index;
        }

        public virtual int GetActivations(Single1D activations, int index)
        {
            return index;
        }


        #endregion
    }
}
