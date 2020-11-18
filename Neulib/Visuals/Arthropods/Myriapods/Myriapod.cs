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
    public class Myriapod : Arthropod
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Myriapod()
        {
        }

        public Myriapod(Stream stream, Serializer serializer) : base(stream, serializer)
        {
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region BaseObject

        protected override void CopyFrom(object o)
        {
            base.CopyFrom(o);
            Myriapod value = o as Myriapod ?? throw new InvalidTypeException(o, nameof(Myriapod), 929472);
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
    }
}
