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

namespace Neulib.Visuals.Arthropods.Myriapods
{
    public class Millipede : Myriapod
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Millipede()
        {
        }

        public Millipede(Stream stream, Serializer serializer) : base(stream, serializer)
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Millipede value = o as Millipede ?? throw new InvalidTypeException(o, nameof(Millipede), 929472);
        }

        public override void WriteToStream(Stream stream, Serializer serializer)
        {
            base.WriteToStream(stream, serializer);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Visual

        public override void Randomize(Random random)
        {
            base.Randomize(random);
            Clear();
            float a1 = (float)random.BoxMuller1(PI/50d);
            float l1 = (float)random.Weibull(20d, 2d);
            float w1 = (float)random.Weibull(25d, 2d);
            int nSegments = (int)random.Weibull(25d, 2d);
            for (int i = 0; i < nSegments; i++)
            {
                Segment segment = new Segment();
                segment.Length = l1;
                segment.Width = w1;
                segment.Angle = a1;
                Add(segment);
            }
        }

        public override void Step(float dt, WorldSettings settings, ProgressReporter reporter, CancellationTokenSource tokenSource)
        {
            base.Step(dt, settings, reporter, tokenSource);
        }

        public override void AddInstructions(InstructionList instructions, Transform transform)
        {
            base.AddInstructions(instructions, transform);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Segmented

        public override void UpdateTransforms()
        {
            base.UpdateTransforms();
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Arthropod

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Myriapod

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Millipede

        #endregion
    }
}
