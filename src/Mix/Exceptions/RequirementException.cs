using System;

namespace Mix.Exceptions
{
    public class RequirementException : Exception
    {
        public string Property { get; private set; }
        public string Description { get; private set; }

        public RequirementException(string message, string property, string description)
            : base(message)
        {
            Property = property ?? String.Empty;
            Description = description ?? String.Empty;
        }
    }
}