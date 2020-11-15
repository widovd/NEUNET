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

namespace Neulib.Bugs
{
    public class Visual : BaseObject
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public Visual Parent { get; set; }

        public Single2 Position { get; set; } = Single2.Zero;

        public Single2x2 Rotation { get; set; } = Single2x2.One;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Visual()
        {
        }

        public Visual(Stream stream, BinarySerializer serializer) : base(stream, serializer)
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

        public virtual void Step(WorldSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
            Thread.Sleep(100);
        }

        protected virtual void AddInstructions(InstructionList instructions)
        {

        }

        protected Single2 Transform(Single2 point)
        {
            point = Position + Rotation * point;
            if (Parent == null)
                return point;
            else
                return Parent.Transform(point);
        }

        public InstructionList GetInstructions()
        {
            InstructionList instructions = new InstructionList();
            AddInstructions(instructions);
            instructions.ForEach(instruction =>
            {
                instruction.Point = Transform(instruction.Point);
            });
            return instructions;
        }

        #endregion
    }
}
