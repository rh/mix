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

        internal ActionExecutionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        internal ActionExecutionException(Exception inner)
            : base("The action could not be executed.", inner)
        {
        }

        internal ActionExecutionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
