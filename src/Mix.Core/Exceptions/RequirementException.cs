using System;

namespace Mix.Core.Exceptions
{
    public class RequirementException : Exception
    {
        private readonly string property = String.Empty;
        private readonly string description = String.Empty;

        public RequirementException(string message, string property, string description)
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