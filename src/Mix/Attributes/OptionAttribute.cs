using System;
using System.Reflection;

namespace Mix.Attributes
{
    /// <summary>
    /// An attribute to mark properties as options.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class OptionAttribute : Attribute
    {
        /// <summary>
        /// Indicates if the property supports XPath templates (e.g. {@name}).
        /// Any XPath templates will be automatically evaluated.
        /// </summary>
        /// <remarks>
        /// Setting this to <c>true</c> is only useful for <c>string</c> properties.
        /// </remarks>
        public bool SupportsXPathTemplates { get; set; }

        public static bool IsDefinedOn(PropertyInfo property)
        {
            return property.IsDefined(typeof(OptionAttribute), false);
        }
    }
}
