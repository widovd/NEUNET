using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Neulib.Exceptions;
using Neulib.Serializers;

namespace Neulib
{

    public class BaseObject : ICloneable, IBinarySerializable
    {
        // ----------------------------------------------------------------------------------------
        #region Properties


        #endregion
        // ----------------------------------------------------------------------------------------
        #region Constructors

        public BaseObject()
        {
        }

        public BaseObject(Stream stream, BinarySerializer serializer)
        { }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region ICloneable

        protected virtual void CopyFrom(object o)
        { }

        protected virtual object CreateNew()
        {
            Type type = GetType();
            if (type == null) throw new VarNullException("type", 846013);
            try
            {
                return Activator.CreateInstance(type);
            }
            catch (MissingMethodException ex)
            {
                throw new InvalidCodeException($"Activator.CreateInstance({type}) failed.", ex, 268442);
            }
        }

        public object Clone()
        {
            BaseObject value = (BaseObject)CreateNew();
            value.CopyFrom(this);
            return value;
        }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region IBinarySerializable

        public virtual void WriteToStream(Stream stream, BinarySerializer serializer)
        { }

        #endregion
        // ----------------------------------------------------------------------------------------
        #region static

        public static void ParallelFor(int fromInclusive, int toExclusive, Action<int> body, bool parallel = true)
        {
            if (parallel)
                Parallel.For(fromInclusive, toExclusive, (int i) => body(i));
            else
                for (int i = fromInclusive; i < toExclusive; i++)
                {
                    body(i);
                }
        }

        #endregion
    }
}
