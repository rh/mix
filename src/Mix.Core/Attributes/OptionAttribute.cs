using System;
using System.Reflection;

namespace Mix.Core.Attributes
{
    /// <summary>
    /// An attribute to mark properties as options.
    /// </summary>
    /// <example>
    /// In the following example, Prepend.Text is marked as an option:
    /// <code>
    /// public class Prepend : Task
    /// {
    ///     [Option]
    ///     public string Text { get; set; }
    ///     // etc.
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class OptionAttribute : Attribute
    {
        public static bool IsDefinedOn(PropertyInfo property)
        {
            return property.IsDefined(typeof(OptionAttribute), false);
        }
    }
}