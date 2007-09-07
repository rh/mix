using System;
using System.Runtime.Serialization;

namespace Mix.Core.Exceptions
{
    [Serializable]
    public class RequirementException : Exception
    {
        private readonly string property = String.Empty;
        private readonly string description = String.Empty;

        public RequirementException()
        {
        }

        public RequirementException(string message)
            : base(message)
        {
        }

        public RequirementException(string message, string property)
            : base(message)
        {
            this.property = property ?? String.Empty;
        }

        public RequirementException(string message, string property, string description)
            : base(message)
        {
            this.property = property ?? String.Empty;
            this.description = description ?? String.Empty;
        }

        public RequirementException(string message, Exception inner)
            : base(message, inner)
        {
        }

    	protected RequirementException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
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