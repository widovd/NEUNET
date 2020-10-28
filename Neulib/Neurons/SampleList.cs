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

        public int NX { get; private set; }
        public int NY { get; private set; }

        //public List<Sample> Samples { get; private set; } = new List<Sample>();


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public SampleList(int nu, int nv, int ny)
        {
            NU = nu;
            NV = nv;
            NX = nu * nv;
            NY = ny;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region SampleList


        #endregion
    }
}
