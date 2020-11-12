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
    /// Contains one or more neurons.
    /// </summary>
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

        /// <summary>
        /// Reads the activation values from the neurons and writes the values to the activations array.
        /// </summary>
        /// <param name="activations">The activation values array.</param>
        /// <param name="index">The start index of the array.</param>
        /// <returns>The updated start index.</returns>
        public virtual int GetActivations(Single1D activations, int index)
        {
            return index;
        }

        /// <summary>
        /// Reads the activation values from the activations array and writes the values to the neurons.
        /// </summary>
        /// <param name="activations">The activation values array.</param>
        /// <param name="index">The start index of the array.</param>
        /// <returns>The updated start index.</returns>
        public virtual int SetActivations(Single1D activations, int index)
        {
            return index;
        }

        /// <summary>
        /// Reads the bias and weight values from the neurons and writes the values to the coefficients array.
        /// </summary>
        /// <param name="coefficients">The bias and weight values array.</param>
        /// <param name="index">The start index of the array.</param>
        /// <returns>The updated start index.</returns>
        public virtual int GetCoefficients(Single1D coefficients, int index)
        {
            return index;
        }

        /// <summary>
        /// Reads the bias and weight values from the activations array and writes the values to the neurons.
        /// </summary>
        /// <param name="activations">The bias and weight values array.</param>
        /// <param name="index">The start index of the array.</param>
        /// <returns>The updated start index.</returns>
        public virtual int SetCoefficients(Single1D coefficients, int index)
        {
            return index;
        }

        /// <summary>
        /// Returns the number of bias and weight values.
        /// </summary>
        /// <returns>The number of bias and weight values.</returns>
        public virtual int CoefficientCount()
        {
            return 0;
        }

        /// <summary>
        /// Creates a new array which can hold the bias and weight values.
        /// </summary>
        /// <returns>The new array.</returns>
        public Single1D CreateCoefficients()
        {
            return new Single1D(CoefficientCount());
        }


        /// <summary>
        /// Creates a new array which holds the bias and weight values.
        /// </summary>
        /// <returns>The new array which holds the bias and weight values.</returns>
        public Single1D GetCoefficients()
        {
            Single1D coefficients = CreateCoefficients();
            GetCoefficients(coefficients, 0);
            return coefficients;
        }

        /// <summary>
        /// Calculates the partial derivatives of the cost function with respect to the bias and weight values
        /// and and adds the values to the derivatives array.
        /// </summary>
        /// <param name="coefficients">The derivatives array will be updated.</param>
        /// <param name="index">The start index of the array.</param>
        /// <param name="lambdaDivN">The regularization parameter lambda / number of weights.</param>
        /// <returns>The updated start index.</returns>
        public virtual int AddDerivatives(Single1D derivatives, int index, float lambdaDivN)
        {
            return index;
        }

        /// <summary>
        /// Randomizes the bias and weight values of the neurons.
        /// </summary>
        /// <param name="random">The random number generator.</param>
        /// <param name="biasMagnitude">The magnitude of the random bias values.</param>
        /// <param name="weightMagnitude">The magnitude of the random weight values.</param>
        public virtual void Randomize(Random random, float biasMagnitude, float weightMagnitude)
        {
        }

        /// <summary>
        /// Returns the number of weight values.
        /// </summary>
        /// <returns>The number of weight values.</returns>
        public virtual int CountWeight()
        {
            return 0;
        }

        /// <summary>
        /// Returns the sum of the weight^2 values.
        /// </summary>
        /// <returns>The sum of the weight^2 values.</returns>
        public virtual float SumWeightSqr()
        {
            return 0f;
        }

        /// <summary>
        /// Clears all connections to any previous layers.
        /// </summary>
        public virtual void ClearConnections()
        {
        }

        /// <summary>
        /// Adds connections to a previous layer.
        /// </summary>
        /// <param name="prevLayer">The previous layer.</param>
        public virtual void AddConnections(Layer prevLayer)
        {
        }

        /// <summary>
        /// Adds connections to a list of previous layers.
        /// </summary>
        /// <param name="prevLayer">The list of previous layers.</param>
        public virtual void AddConnections(LayerList prevLayerList)
        {
        }


        public void ClearAndAddConnections(Layer prevLayer)
        {
            ClearConnections();
            AddConnections(prevLayer);
        }

        #endregion
    }
}
