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
    public class SignedArray : BaseArray
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private readonly sbyte[] _values;

        public sbyte this[long i]
        {
            get => _values[i];
            set => _values[i] = value;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public SignedArray(params int[] lengths) : base(lengths)
        {
            _values = new sbyte[LongLength];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseIdx

        public override void ReadValuesFromStream(Stream stream)
        {
            ForEach(i => _values[i] = (sbyte)stream.ReadByte());
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region SignedByteIdx

        #endregion
    }
}
