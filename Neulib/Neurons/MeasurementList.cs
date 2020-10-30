using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Numerics;

namespace Neulib.Neurons
{
    public class MeasurementList : List<Single1D>
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public MeasurementList(int count, int ny) : base(count)
        {
            for (int i = 0; i < count; i++)
            {
                Add(new Single1D(ny));
            }
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        #endregion
    }
}
