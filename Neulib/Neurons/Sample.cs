using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neulib.Neurons
{
    /// <summary>
    /// Represents one sample row of x (input) and y (output) values.
    /// </summary>
    public class Sample
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Input for the first layer in the network.
        /// </summary>
        public float[] Xs { get; private set; }

        /// <summary>
        /// Required output of the last layer in the network.
        /// </summary>
        public float[] Ys { get; private set; }

        /// <summary>
        /// Realized output of the last layer in the network by feedforward.
        /// </summary>
        public float[] Zs { get; private set; }

        /// <summary>
        /// A reference to the source of this sample.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// The required output value as a (training set-) label.
        /// </summary>
        public byte Label { get; set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Sample(int nx, int ny)
        {
            Xs = new float[nx];
            Ys = new float[ny];
            Zs = new float[ny];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Sample


        #endregion
    }
}
