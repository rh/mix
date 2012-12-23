using System;
using System.Reflection;

namespace Mix.Attributes
{
    /// <summary>
    /// An attribute to mark properties with a description.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class DescriptionAttribute : Attribute
    {
        public string Description { get; private set; }

        public DescriptionAttribute(string description)
        {
            Description = description;
        }

        public static bool IsDefinedOn(object obj)
        {
            return obj.GetType().IsDefined(typeof(DescriptionAttribute), false);
        }

        public static bool IsDefinedOn(PropertyInfo property)
        {
            return property.IsDefined(typeof(DescriptionAttribute), false);
        }

        public static string GetDescriptionFrom(object obj, string defaultValue)
        {
            if (IsDefinedOn(obj))
            {
                var attribute = (DescriptionAttribute) obj.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
                return attribute.Description;
            }
            return defaultValue;
        }

        public static string GetDescriptionFrom(PropertyInfo property, string defaultValue)
        {
            if (IsDefinedOn(property))
            {
                var attribute = (DescriptionAttribute) property.GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
                return attribute.Description;
            }
            return defaultValue;
        }
    }
}
