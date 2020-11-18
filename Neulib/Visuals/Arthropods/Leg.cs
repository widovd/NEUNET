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
    public class Leg : Segmented
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Angle of this leg with respect to the parent segment. Zero means perpendicular.
        /// </summary>
        public float Angle { get; set; } = 0f;


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Leg()
        {
        }

        public Leg(int nSegments)
        {
            for (int i = 0; i < nSegments; i++)
            {
                Add(new Moveable(new Segment()));
            }
        }



        public Leg(Stream stream, Serializer serializer) : base(stream, serializer)
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

        public override void AddInstructions(InstructionList instructions, Transform transform)
        {
            base.AddInstructions(instructions, transform);
        }


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Bug

        #endregion
    }
}
