using System;
using System.Reflection;

namespace Mix.Core.Attributes
{
    /// <summary>
    /// An attribute to mark properties as required.
    /// </summary>
    /// <example>
    /// In the following example, PrependAction.Text is marked as required:
    /// <code>
    /// public class PrependAction : Action
    /// {
    ///     [Required]
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
    public class RequiredAttribute : Attribute
    {
        public static bool IsDefinedOn(PropertyInfo property)
        {
            return property.IsDefined(typeof(RequiredAttribute), false);
        }
    }
}