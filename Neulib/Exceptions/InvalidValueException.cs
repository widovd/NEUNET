using System;
using System.Runtime.Serialization;

namespace Neulib.Exceptions
{
    [Serializable]
    public class InvalidValueException : InvalidCodeException
    {
        private static string GetMessage(string name, object value)
        {
            string valueStr = value == null ? "null" : value.ToString();
            return $"Invalid {name}: {valueStr}.";
        }

        public InvalidValueException()
        {
        }

        public InvalidValueException(string message) : base(message)
        {
        }

        public InvalidValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidValueException(string message, int code) : base(message, code)
        {
        }

        public InvalidValueException(string message, Exception innerException, int code) : base(message, innerException, code)
        {
        }

        public InvalidValueException(string name, object value) : base(GetMessage(name, value))
        {
        }

        public InvalidValueException(string name, object value, int code) : base(GetMessage(name, value), code)
        {
        }

        public InvalidValueException(string name, object value, Exception innerException) : base(GetMessage(name, value), innerException)
        {
        }

        public InvalidValueException(string name, object value, Exception innerException, int code) : base(GetMessage(name, value), innerException, code)
        {
        }

        protected InvalidValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
