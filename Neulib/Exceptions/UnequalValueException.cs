using System;
using System.Runtime.Serialization;

namespace Neulib.Exceptions
{
    [Serializable]
    public class UnequalValueException : InvalidCodeException
    {
        private static string GetMessage(object value1, object value2)
        {
            return $"{value1} != {value2}.";
        }

        public UnequalValueException()
        {
        }

        public UnequalValueException(string message) : base(message)
        {
        }

        public UnequalValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UnequalValueException(string message, int code) : base(message, code)
        {
        }

        public UnequalValueException(string message, Exception innerException, int code) : base(message, innerException, code)
        {
        }

        public UnequalValueException(object value1, object value2) : base(GetMessage(value1, value2))
        {
        }

        public UnequalValueException(object value1, object value2, int code) : base(GetMessage(value1, value2), code)
        {
        }

        public UnequalValueException(object value1, object value2, Exception innerException) : base(GetMessage(value1, value2), innerException)
        {
        }

        public UnequalValueException(object value1, object value2, Exception innerException, int code) : base(GetMessage(value1, value2), innerException, code)
        {
        }

        protected UnequalValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
