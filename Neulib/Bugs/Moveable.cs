using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Neulib.Exceptions;
using Neulib.Serializers;
using Neulib.Numerics;
using Neulib.Instructions;

namespace Neulib.Bugs
{
    public class Moveable: Visual
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public Single2 Position { get; set; }

        public Single2x2 Rotation { get; set; }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Moveable()
        {
        }

        public Moveable(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
            Position = stream.ReadSingle2();
            Rotation = stream.ReadSingle2x2();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Moveable value = o as Moveable ?? throw new InvalidTypeException(o, nameof(Moveable), 508235);
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

        public override void Step(WorldSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
            base.Step(settings, reporter, tokenSource);
        }

        protected override void AddInstructions(InstructionList instructions)
        {

        }

        #endregion

    }
}
