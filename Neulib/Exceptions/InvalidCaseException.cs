using System;
using System.Runtime.Serialization;

namespace Neulib.Exceptions
{
    [Serializable]
    public class InvalidCaseException : InvalidCodeException
    {
        private static string GetMessage(string name, object value)
        {
            return $"Invalid case: {name} = {value}.";
        }

        public InvalidCaseException()
        {
        }

        public InvalidCaseException(string message) : base(message)
        {
        }

        public InvalidCaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidCaseException(string message, int code) : base(message, code)
        {
        }

        public InvalidCaseException(string message, Exception innerException, int code) : base(message, innerException, code)
        {
        }

        public InvalidCaseException(string name, object value) : base(GetMessage(name, value))
        {
        }

        public InvalidCaseException(string name, object value, int code) : base(GetMessage(name, value), code)
        {
        }

        public InvalidCaseException(string name, object value, Exception innerException) : base(GetMessage(name, value), innerException)
        {
        }

        public InvalidCaseException(string name, object value, Exception innerException, int code) : base(GetMessage(name, value), innerException, code)
        {
        }

        protected InvalidCaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
