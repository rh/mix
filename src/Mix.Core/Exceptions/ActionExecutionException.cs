using System;
using System.Runtime.Serialization;

namespace Mix.Core.Exceptions
{
    [Serializable]
    public class ActionExecutionException : Exception
    {
        public ActionExecutionException()
        {
        }

        public ActionExecutionException(string message)
            : base(message)
        {
        }

        public ActionExecutionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ActionExecutionException(Exception inner)
            : base("The action could not be executed.", inner)
        {
        }

        public ActionExecutionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}