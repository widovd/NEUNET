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
    public class ByteArray : BaseArray
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private readonly byte[] _values;

        public byte this[long i]
        {
            get => _values[i];
            set => _values[i] = value;
        }

        public byte this[params int[] indices]
        {
            get => _values[GetLongIndex(indices)];
            set => _values[GetLongIndex(indices)] = value;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public ByteArray(params int[] lengths) : base(lengths)
        {
            _values = new byte[LongLength];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseIdx

        public override void ReadValuesFromStream(Stream stream)
        {
            ForEach(i => _values[i] = (byte)stream.ReadByte());
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ByteIdx

        #endregion
    }
}
