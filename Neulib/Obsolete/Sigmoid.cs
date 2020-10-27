using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// Creates a new Sigmoid neuron which is connected to a previous layer.
        /// </summary>
        /// <param name="count">The number of neurons in the previous layer, or 0 if there is no previous layer.</param>
        public Sigmoid(int count) : base(count)
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Neuron

        /// <summary>
        /// The Sigmoid activation function.
        /// </summary>
        /// <param name="z">Input value z</param>
        /// <returns>1 / (1 + exp(-z))</returns>
        protected override float ActivationFunction(float z)
        {
            return 1f / (1f + (float)Exp(-z));
        }

        [Obsolete]
        public override float DerivativeOld(float yjk)
        {
            float r = Activation;
            return r * (1f - r) * (r - yjk);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Sigmoid

        #endregion
    }
}
