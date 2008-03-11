using System;

namespace Mix.Core.Exceptions
{
    public class ValidationException : Exception
    {
        private readonly string property = String.Empty;
        private readonly string description = String.Empty;

        public ValidationException(string message, string property, string description)
            : base(message)
        {
            this.property = property ?? String.Empty;
            this.description = description ?? String.Empty;
        }

        public string Property
        {
            get { return property; }
        }

        public string Description
        {
            get { return description; }
        }
    }
}