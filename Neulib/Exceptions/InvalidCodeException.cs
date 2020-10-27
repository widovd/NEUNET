using System;
using System.Runtime.Serialization;

namespace Neulib.Exceptions
{
    [Serializable]
    public class InvalidCodeException : BaseException
    // This and derived exceptions are thrown when the writer of the code has made a mistake (not the end user).
    // In most cases they indicate a bug, and not a wrong input.
    {
        public InvalidCodeException()
        {
        }

        public InvalidCodeException(string message) : base(message)
        {
        }

        public InvalidCodeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidCodeException(string message, int code) : base(message, code)
        {
        }

        public InvalidCodeException(string message, Exception innerException, int code) : base(message, innerException, code)
        {
        }

        protected InvalidCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
