using System;

namespace Mix.Exceptions
{
    public class ValidationException : Exception
    {
        public string Property { get; private set; }
        public string Description { get; private set; }

        public ValidationException(string message, string property, string description)
            : base(message)
        {
            Property = property ?? string.Empty;
            Description = description ?? string.Empty;
        }
    }
}
