using System;
using System.Runtime.Serialization;

namespace Mix.Console.Exceptions
{
    public class InvalidPathException : Exception
    {
        public InvalidPathException()
        {
        }

        public InvalidPathException(string message)
            : base(message)
        {
        }

        public InvalidPathException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public InvalidPathException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}