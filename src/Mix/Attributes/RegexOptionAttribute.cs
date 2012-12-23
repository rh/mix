using System;
using System.Reflection;

namespace Mix.Attributes
{
    /// <summary>
    /// An attribute to mark properties as a regular expression option.
    /// Properties marked with this attribute will be automatically validated.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class RegexOptionAttribute : Attribute
    {
        public static bool IsDefinedOn(PropertyInfo property)
        {
            return property.IsDefined(typeof(RegexOptionAttribute), false);
        }
    }
}
