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
using Neulib.Extensions;
using Neulib.Serializers;
using Neulib.Numerics;
using Neulib.Instructions;
using static System.Math;

namespace Neulib.Visuals.Arthropods
{
    public class Segment : Visual
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public float Width { get; set; } = 20f;

        public float Length { get; set; } = 30f;

        public float Angle { get; set; } = 0.2f;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Segment()
        {
        }

        public Segment(Stream stream, Serializer serializer) : base(stream, serializer)
        {
            Width = stream.ReadSingle();
            Length = stream.ReadSingle();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Segment value = o as Segment ?? throw new InvalidTypeException(o, nameof(Segment), 403658);
            Width = value.Width;
            Length = value.Length;
        }

        public override void WriteToStream(Stream stream, Serializer serializer)
        {
            base.WriteToStream(stream, serializer);
            stream.WriteSingle(Width);
            stream.WriteSingle(Length);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Visual

        public override void Randomize(Random random)
        {
            base.Randomize(random);
        }

        float da = 0f;

        public override void Step(float dt, WorldSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
            base.Step(dt, settings, reporter, tokenSource);
            float dda = - Angle;
            da += dda * dt;
            Angle += da * dt;
        }


        public override void AddInstructions(InstructionList instructions, Transform transform)
        {
            base.AddInstructions(instructions, transform);
            float w2 = Width / 2;
            float l = Length;
            instructions.Add(new Instruction(0, -w2, InstructionEnum.Add, transform));
            instructions.Add(new Instruction(0, w2, InstructionEnum.Add, transform));
            instructions.Add(new Instruction(l, w2, InstructionEnum.Add, transform));
            instructions.Add(new Instruction(l, -w2, InstructionEnum.Polygon, transform));
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Segment

        #endregion
    }
}
