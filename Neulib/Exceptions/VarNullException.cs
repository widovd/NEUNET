using System;
using System.Runtime.Serialization;

namespace Neulib.Exceptions
{
    [Serializable]
    public class VarNullException : InvalidCodeException
    {
        private static string GetMessage(string name)
        {
            return $"{name} is null.";
        }

        public VarNullException()
        {
        }

        public VarNullException(string name) : base(GetMessage(name))
        {
        }

        public VarNullException(string name, int code) : base(GetMessage(name), code)
        {
        }

        public VarNullException(string name, Exception innerException) : base(GetMessage(name), innerException)
        {
        }

        public VarNullException(string name, Exception innerException, int code) : base(GetMessage(name), innerException, code)
        {
        }

        protected VarNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
