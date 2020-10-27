using System;
using System.Runtime.Serialization;

namespace Neulib.Exceptions
{
    [Serializable]
    public class SingularMatrixException : UserException
    {

        private static string GetMessage()
        {
            return $"Singular matrix.";
        }

        public SingularMatrixException()
        {
        }

        public SingularMatrixException(string message) : base(message)
        {
        }

        public SingularMatrixException(string message, int code) : base(message, code)
        {
        }

        public SingularMatrixException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public SingularMatrixException(string message, Exception innerException, int code) : base(message, innerException, code)
        {
        }

        public SingularMatrixException(int code) : base(GetMessage(), code)
        {
        }

        public SingularMatrixException(Exception innerException, int code) : base(GetMessage(), innerException, code)
        {
        }

        protected SingularMatrixException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
