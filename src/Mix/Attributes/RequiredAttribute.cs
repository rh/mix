using System;
using System.Reflection;

namespace Mix.Attributes
{
    /// <summary>
    /// An attribute to mark properties as required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class RequiredAttribute : Attribute
    {
        public static bool IsDefinedOn(PropertyInfo property)
        {
            return property.IsDefined(typeof(RequiredAttribute), false);
        }
    }
}
