using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Numerics;

namespace Neulib.Neurons
{
    /// <summary>
    /// Represents one sample row of input (x) and output (y) values.
    /// </summary>
    public class Sample
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Input for the first layer in the network.
        /// </summary>
        public Single1D Inputs { get; private set; }

        /// <summary>
        /// Required output of the last layer in the network.
        /// </summary>
        public Single1D Requirements { get; private set; }

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

        public Sample(int nInput, int nRequirements)
        {
            Inputs = new Single1D(nInput);
            Requirements = new Single1D(nRequirements);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Sample


        #endregion
    }
}
