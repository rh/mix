using System;
using System.Reflection;

namespace Mix.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class ExampleAttribute : Attribute
    {
        private readonly string example;

        public ExampleAttribute(string example)
        {
            this.example = example;
        }

        public string Example
        {
            get { return example; }
        }

        public static bool IsDefinedOn(object obj)
        {
            return obj.GetType().IsDefined(typeof(ExampleAttribute), false);
        }

        public static bool IsDefinedOn(PropertyInfo property)
        {
            return property.IsDefined(typeof(ExampleAttribute), false);
        }

        public static string GetExampleFrom(object obj)
        {
            if (IsDefinedOn(obj))
            {
                ExampleAttribute attribute =
                    (ExampleAttribute)
                    obj.GetType().GetCustomAttributes(typeof(ExampleAttribute), false)[0];
                return attribute.Example;
            }
            return String.Empty;
        }

        public static string GetExampleFrom(PropertyInfo property)
        {
            if (IsDefinedOn(property))
            {
                ExampleAttribute attribute =
                    (ExampleAttribute)
                    property.GetCustomAttributes(typeof(ExampleAttribute), false)[0];
                return attribute.Example;
            }
            return String.Empty;
        }
    }
}
