using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Mix.Core.Exceptions
{
    [Serializable]
    [DebuggerStepThrough]
    public sealed class ActionExecutionException : Exception
    {
        internal ActionExecutionException()
        {
        }

        internal ActionExecutionException(string message)
            : base(message)
        {
        }

        internal ActionExecutionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        internal ActionExecutionException(Exception innerException)
            : base("The action could not be executed.", innerException)
        {
        }

        internal ActionExecutionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
