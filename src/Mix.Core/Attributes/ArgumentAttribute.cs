using System;
using System.Reflection;

namespace Mix.Core.Attributes
{
    /// <summary>
    /// An attribute to mark properties as arguments.
    /// </summary>
    /// <example>
    /// In the following example, PrependAction.Text is marked as an argument:
    /// <code>
    /// public class PrependAction : Action
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
    [AttributeUsage(AttributeTargets.Property)]
    public class ArgumentAttribute : Attribute
    {
        public static bool IsDefinedOn(PropertyInfo property)
        {
            return property.IsDefined(typeof(ArgumentAttribute), true);
        }
    }
}