using System;
using System.Runtime.Serialization;

namespace Neulib.Exceptions
{
    [Serializable]
    public class BaseException : Exception
    // Every exception thrown in Fresh is derived from this type.
    // Do not throw this exception. Use one of the derived exception types.
    {

        // A unique identifier of the exception, generated with a random number generator. Range 100000 - 999999.
        public int ErrorCode { get; private set; }

        public BaseException()
        {
        }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BaseException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public BaseException(string message, Exception innerException, int errorCode) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        private const string _CodeId = "Code";

        protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            foreach (SerializationEntry entry in info)
            {
                switch (entry.Name)
                {
                    case _CodeId:
                        ErrorCode = (int)entry.Value;
                        break;
                }
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(_CodeId, ErrorCode);
        }


        public override string ToString()
        {
            if (ErrorCode == 0)
                return Message;
            else
                return $"Error {ErrorCode}: '{Message}'";
        }
    }
}
