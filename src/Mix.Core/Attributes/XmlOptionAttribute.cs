using System;
using System.Reflection;

namespace Mix.Core.Attributes
{
    /// <summary>
    /// An attribute to mark properties as an XML option.
    /// Properties marked with this attribute will be automatically validated.
    /// </summary>
    /// <example>
    /// In the following example, AddFragment.Fragment is marked as an XML option:
    /// <code>
    /// public class AddFragment : Task
    /// {
    ///     [Option, XmlOption]
    ///     public string Fragment { get; set; }
    ///     // etc.
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class XmlOptionAttribute : Attribute
    {
        public static bool IsDefinedOn(PropertyInfo property)
        {
            return property.IsDefined(typeof(XmlOptionAttribute), false);
        }
    }
}