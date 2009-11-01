using System;

namespace Mix.Core.Exceptions
{
    public class XPathTemplateException : Exception
    {
        public string Property { get; private set; }
        public string Value { get; private set; }

        public XPathTemplateException(string message, string property, string value)
            : base(message)
        {
            Property = property ?? String.Empty;
            Value = value ?? String.Empty;
        }
    }
}