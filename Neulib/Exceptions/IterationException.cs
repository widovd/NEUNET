using System;
using System.Runtime.Serialization;

namespace Neulib.Exceptions
{
    [Serializable]
    public class IterationException : UserException
    {

        private static string GetMessage()
        {
            return "Too many iterations.";
        }

        public IterationException()
        {
        }

        public IterationException(string message) : base(message)
        {
        }

        public IterationException(string message, int code) : base(message, code)
        {
        }

        public IterationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public IterationException(string message, Exception innerException, int code) : base(message, innerException, code)
        {
        }

        public IterationException(int code) : base(GetMessage(), code)
        {
        }

        public IterationException(Exception innerException, int code) : base(GetMessage(), innerException, code)
        {
        }

        protected IterationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
