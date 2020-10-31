using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neulib.Neurons
{
    public class SampleList: List<Sample>
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public int NU { get; private set; }

        public int NV { get; private set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public SampleList(int nu, int nv)
        {
            NU = nu;
            NV = nv;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region SampleList

        #endregion
    }
}
