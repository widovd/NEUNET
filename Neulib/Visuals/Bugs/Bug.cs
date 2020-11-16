using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Serializers;
using Neulib.Numerics;
using Neulib.Instructions;

namespace Neulib.Visuals.Bugs
{
    public class Bug : Visual
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Bug()
        {
        }

        public Bug(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Visual value = o as Visual ?? throw new InvalidTypeException(o, nameof(Visual), 610504);
            Position = value.Position;
            Rotation = value.Rotation;
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
            stream.WriteSingle2(Position);
            stream.WriteSingle2x2(Rotation);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Visual

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Bug

        #endregion
    }
}
