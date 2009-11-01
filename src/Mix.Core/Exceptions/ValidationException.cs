using System;

namespace Mix.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public string Property { get; private set; }
        public string Description { get; private set; }

        public ValidationException(string message, string property, string description)
            : base(message)
        {
            Property = property ?? String.Empty;
            Description = description ?? String.Empty;
        }
    }
}