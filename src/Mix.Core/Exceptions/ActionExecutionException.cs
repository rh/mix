using System;

namespace Mix.Core.Exceptions
{
    public class ActionExecutionException : Exception
    {
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
    }
}