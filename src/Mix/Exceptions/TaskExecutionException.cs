using System;

namespace Mix.Exceptions
{
    public class TaskExecutionException : Exception
    {
        public TaskExecutionException(string message)
            : base(message)
        {
        }

        public TaskExecutionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public TaskExecutionException(Exception inner)
            : base(inner.Message, inner)
        {
        }
    }
}