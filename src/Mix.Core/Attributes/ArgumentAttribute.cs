using System;
using System.Reflection;

namespace Mix.Core.Attributes
{
    /// <summary>
    /// An attribute to mark properties as arguments.
    /// </summary>
    /// <example>
    /// In the following example, Prepend.Text is marked as an argument:
    /// <code>
    /// public class Prepend : Task
    /// {
    ///     [Argument]
    ///     public string Text
    ///     {
    ///         get { return text; }
    ///         set { text = value; }
    ///     }
    ///     // etc.
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class ArgumentAttribute : Attribute
    {
        public static bool IsDefinedOn(PropertyInfo property)
        {
			return property.IsDefined(typeof(ArgumentAttribute), false);
        }
    }
}