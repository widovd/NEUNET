using System;
using System.Runtime.Serialization;

namespace Neulib.Exceptions
{
    [Serializable]
    public class FileFormatException : UserException
    {

        private static string GetMessage()
        {
            return "Wrong file format.";
        }

        public FileFormatException()
        {
        }

        public FileFormatException(string message) : base(message)
        {
        }

        public FileFormatException(string message, int code) : base(message, code)
        {
        }

        public FileFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public FileFormatException(string message, Exception innerException, int code) : base(message, innerException, code)
        {
        }

        public FileFormatException(int code) : base(GetMessage(), code)
        {
        }

        public FileFormatException(Exception innerException, int code) : base(GetMessage(), innerException, code)
        {
        }

        protected FileFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}
