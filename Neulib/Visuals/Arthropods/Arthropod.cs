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
    public class Arthropod : Visual
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public float Width { get; set; } = 20f;
        public float Height { get; set; } = 30f;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Arthropod()
        {
        }

        public Arthropod(Stream stream, BinarySerializer serializer) : base(stream, serializer)
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Arthropod value = o as Arthropod ?? throw new InvalidTypeException(o, nameof(Arthropod), 554610);
        }

        public override void WriteToStream(Stream stream, BinarySerializer serializer)
        {
            base.WriteToStream(stream, serializer);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Visual

        public override void Randomize(Random random)
        {
            base.Randomize(random);
            Width = 10f + 50f * (float)random.NextDouble();
            Height = 10f + 50f * (float)random.NextDouble();
            Single2 translation = new Single2(
                1200f * (float)random.NextDouble(),
                800f * (float)random.NextDouble()
                );
            Single2x2 rotation = Single2x2.Rot1(
                (float)(2d * PI * random.NextDouble())
                );
            Parent.Transform = new Transform(translation, rotation);
        }

        public override void AddInstructions(InstructionList instructions, Transform transform)
        {
            base.AddInstructions(instructions, transform);
            int n = 72;
            for (int i = 0; i <= n; i++)
            {
                double a = 2d * PI * i / n;
                double r = 1d + 0.3d * Cos(3 * a) + 0.2d * Cos(10 * a);
                double x = r * Cos(a);
                double y = r * Sin(a);
                instructions.Add(new Instruction((float)x * Width, (float)y * Height, i < n ? InstructionEnum.Add : InstructionEnum.Polygon, transform));
            }

        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Bug

        #endregion
    }
}
