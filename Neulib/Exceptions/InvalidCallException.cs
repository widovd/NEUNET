using System;
using System.Runtime.Serialization;

namespace Neulib.Exceptions
{
    [Serializable]
    public class InvalidCallException : InvalidCodeException
    {
        private static string GetMessage(string name)
        {
            return $"The call to method '{name}' is not applicable.";
        }

        public InvalidCallException()
        {
        }

        public InvalidCallException(string name) : base(GetMessage(name))
        {
        }

        public InvalidCallException(string name, int code) : base(GetMessage(name), code)
        {
        }

        public InvalidCallException(string name, Exception innerException) : base(GetMessage(name), innerException)
        {
        }

        public InvalidCallException(string name, Exception innerException, int code) : base(GetMessage(name), innerException, code)
        {
        }

        protected InvalidCallException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
