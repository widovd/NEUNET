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
    public class ShortArray : BaseArray
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private readonly short[] _values;

        public short this[long i]
        {
            get => _values[i];
            set => _values[i] = value;
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public ShortArray(params int[] lengths) : base(lengths)
        {
            _values = new short[LongLength];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseIdx

        public override void ReadValuesFromStream(Stream stream)
        {
            ForEach(i => _values[i] = stream.ReadShort(true));
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region ShortIdx

        #endregion
    }
}
