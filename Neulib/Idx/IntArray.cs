using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neulib.Exceptions;
using static System.Convert;
using static System.BitConverter;
using Neulib.Serializers;

namespace Neulib.MultiArrays
{
    public class IntArray : BaseArray
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private readonly int[] _values;

        public int this[long i]
        {
            get => _values[i];
            set => _values[i] = value;
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public IntArray(params int[] lengths) : base(lengths)
        {
            _values = new int[LongLength];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseIdx

        public override void ReadValuesFromStream(Stream stream)
        {
            ForEach(i => _values[i] = stream.ReadInt(true));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IntIdx

        #endregion
    }
}
