using System;
using System.Runtime.Serialization;

namespace Neulib.Exceptions
{
    [Serializable]
    public class UserException : BaseException
    // This and derived exceptions are thrown when the end user made a mistake.
    // In most cases this doesn't indicate a bug, but a wrong input.
    {
        public UserException()
        {
        }

        public UserException(string message) : base(message)
        {
        }

        public UserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UserException(string message, int code) : base(message, code)
        {
        }

        public UserException(string message, Exception innerException, int code) : base(message, innerException, code)
        {
        }

        protected UserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
