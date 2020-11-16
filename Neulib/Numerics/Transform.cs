using System;
using System.IO;
using System.Collections;
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

namespace Neulib.Numerics
{
    public struct Transform
    {
        // ----------------------------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Translation with respect to the parent.
        /// </summary>
        public Single2 Translation;

        /// <summary>
        /// First the rotation is applied, then the translation.
        /// </summary>
        public Single2x2 Rotation;

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public Transform(Single2 position, Single2x2 rotation)
        {
            Translation = position;
            Rotation = rotation;
        }

        public static Transform Default
        {
            get => new Transform(Single2.Zero, Single2x2.One);
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Object

        public override bool Equals(object o)
        {
            Transform value = (Transform)o;
            if (Translation != value.Translation) return false;
            if (Rotation != value.Rotation) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int h = 17;
            unchecked
            {
                h = h * 23 + Translation.GetHashCode();
                h = h * 23 + Rotation.GetHashCode();
            }
            return h;
        }

        public override string ToString()
        {
            return $"{Translation}, {Rotation}";
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region Operators

        public static bool operator ==(Transform left, Transform right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Transform left, Transform right)
        {
            return !left.Equals(right);
        }

        public static Transform operator *(Transform left, Transform right)
        {
            return new Transform(
                left.Translation + left.Rotation * right.Translation, 
                left.Rotation * right.Rotation
                );
        }

        public static Single2 operator *(Transform left, Single2 right)
        {
            return left.Translation + left.Rotation * right;
        }

        #endregion
    }
}
