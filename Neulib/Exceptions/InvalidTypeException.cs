using System;
using System.Runtime.Serialization;
using Neulib.Extensions;

namespace Neulib.Exceptions
{
    [Serializable]
    public class InvalidTypeException : InvalidCodeException
    {
        private static string GetMessage(object value, string name)
        {
            string typeString = value != null ? value.GetType().ToString().Last('.') : "null";
            return $"Type {typeString} is not derived from {name}.";
        }

        public InvalidTypeException()
        {
        }

        public InvalidTypeException(string message) : base(message)
        {
        }

        public InvalidTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidTypeException(string message, int code) : base(message, code)
        {
        }

        public InvalidTypeException(string message, Exception innerException, int code) : base(message, innerException, code)
        {
        }

        public InvalidTypeException(object value, string name) : base(GetMessage(value, name))
        {
        }

        public InvalidTypeException(object value, string name, int code) : base(GetMessage(value, name), code)
        {
        }

        public InvalidTypeException(object value, string name, Exception innerException) : base(GetMessage(value, name), innerException)
        {
        }

        public InvalidTypeException(object value, string name, Exception innerException, int code) : base(GetMessage(value, name), innerException, code)
        {
        }

        protected InvalidTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
