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
    // Collection of Segmented
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Width of this segment.
        /// </summary>
        public float Width { get; set; } = 20f;

        /// <summary>
        /// Length of this segment.
        /// </summary>
        public float Length { get; set; } = 30f;

        /// <summary>
        /// Angle of this segment with respect to the previous segment.
        /// </summary>
        public float Angle { get; set; } = 0.2f;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Segment()
        {
        }

        public Segment(int nLegs, int nSegments)
        {
            for (int i = 0; i < nLegs; i++)
            {
                Add(new Moveable(new Leg(nSegments)));
            }
        }


        public Segment(Stream stream, Serializer serializer) : base(stream, serializer)
        {
            Width = stream.ReadSingle();
            Length = stream.ReadSingle();
            Angle = stream.ReadSingle();
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
            Angle = value.Angle;
        }

        public override void WriteToStream(Stream stream, Serializer serializer)
        {
            base.WriteToStream(stream, serializer);
            stream.WriteSingle(Width);
            stream.WriteSingle(Length);
            stream.WriteSingle(Angle);
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
            float dda = -Angle;
            da += dda * dt;
            Angle += da * dt;
        }


        public override void AddInstructions(InstructionList instructions, Transform transform)
        {
            base.AddInstructions(instructions, transform);
            float w2 = Width / 2;
            float l = Length;
            Polygon polygon = new Polygon(Color.Black, 1f)
            {
                transform.Apply(0, -w2 ),
                transform.Apply(0, w2 ),
                transform.Apply(l, w2 ),
                transform.Apply(l, -w2 ),
            };
            instructions.Add(polygon);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Segment

        public override void UpdateTransforms()
        // Zet ze 2 aan 2 naast elkaar
        {
            base.UpdateTransforms();
            float w2 = 0.5f * Width;
            float pi2 = (float)(0.5d * PI);
            int count = Count;
            for (int i = 0; i < count; i++)
            {
                Moveable moveable = this[i];
                Leg leg = moveable.Visual as Leg ?? throw new VarNullException(nameof(leg), 896174);
                float sgn = i % 2 == 0 ? 1 : -1;
                float x = (1 + 2 * (i / 2)) * Length / count;
                moveable.Transform = new Transform(
                    new Single2(x, sgn * w2),
                    Single2x2.Rotation(sgn * (pi2 + leg.Angle))
                    );
            }
        }

        #endregion
    }
}
