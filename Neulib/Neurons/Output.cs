using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neulib.Neurons
{
    /// <summary>
    /// Represents the activation values of the last layer in a network.
    /// </summary>
    public class Output
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Activation values of the last layer in a network calculated with feedforward.
        /// </summary>
        private readonly float[] _values;

        public float this[int i]
        {
            get { return _values[i]; }
            set { _values[i] = value; }
        }

        public int Count
        {
            get => _values.Length;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Output(int count)
        {
            _values = new float[count];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Activations


        #endregion
    }
}
