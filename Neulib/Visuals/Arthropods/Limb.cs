﻿using System;
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
    public class Limb : Visual
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        public float Width { get; set; } = 20f;
        public float Height { get; set; } = 30f;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Limb()
        {
        }

        public Limb(Stream stream, BinarySerializer serializer) : base(stream, serializer)
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