using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Serializers;
using static System.Math;

namespace Neulib.Neurons
{
    public class Sigmoid : Neuron
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new sigmoid neuron.
        /// </summary>
        public Sigmoid()
        {
        }

        /// <summary>
        /// Creates a new neuron from the stream.
        /// </summary>
        public Sigmoid(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Neuron

        protected override void CalculateActivation()
        {
            Activation = 1f / (1f + (float)Exp(-Sum));
        }

        protected override float ActivationDerivative()
        {
            return Activation * (1f - Activation);
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Sigmoid

        #endregion
    }
}
