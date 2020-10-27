using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neulib.Neurons
{
    /// <summary>
    /// Represents a layer of sigmoid neurons in a neural network.
    /// </summary>
    public class SigmoidLayer : Layer
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Creates a new layer of neurons.
        /// </summary>
        /// <param name="count">The number of neurons in this layer.</param>
        public SigmoidLayer(int count) : base(count)
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Layer

        public override void InitializeNeurons()
        {
            int count = Count;
            int prevCount = Previous != null ? Previous.Count : 0;
            for (int i = 0; i < count; i++)
            {
                _neurons[i] = new Sigmoid(prevCount);
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region SigmoidLayer

        #endregion
    }
}
