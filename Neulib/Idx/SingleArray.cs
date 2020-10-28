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
    public class SingleArray : BaseArray
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        private readonly float[] _values;

        public float this[long i]
        {
            get => _values[i];
            set => _values[i] = value;
        }

        public float this[params int[] indices]
        {
            get => _values[GetLongIndex(indices)];
            set => _values[GetLongIndex(indices)] = value;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public SingleArray(params int[] lengths) : base(lengths)
        {
            _values = new float[LongLength];
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseIdx

        public override void ReadValuesFromStream(Stream stream)
        {
            ForEach(i => _values[i] = stream.ReadSingle(true));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region SingleIdx

        #endregion
    }
}
