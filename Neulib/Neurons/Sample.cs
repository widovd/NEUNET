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

        public float[] Xs { get; private set; }
        public float[] Ys { get; private set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Sample(int nx, int ny)
        {
            Xs = new float[nx];
            Ys = new float[ny];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Sample


        #endregion
    }
}
