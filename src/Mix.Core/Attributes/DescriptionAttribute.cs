using System;
using System.Reflection;

namespace Mix.Core.Attributes
{
    /// <summary>
    /// An attribute to mark properties with a description.
    /// </summary>
    /// <example>
    /// <code>
    /// [Description("Prepends text to...")]
    /// public sealed class PrependAction : Action
    /// {
    ///     [Description("The text to prepend.")]
    ///     public string Text
    ///     {
    ///         get { return text; }
    ///         set { text = value; }
    ///     }
    ///     // etc.
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public sealed class DescriptionAttribute : Attribute
    {
        private readonly string description;

        public DescriptionAttribute(string description)
        {
            this.description = description;
        }

        public string Description
        {
            get { return description; }
        }

        public static bool IsDefinedOn(object obj)
        {
            return obj.GetType().IsDefined(typeof(DescriptionAttribute), false);
        }

        public static bool IsDefinedOn(PropertyInfo property)
        {
            return property.IsDefined(typeof(DescriptionAttribute), false);
        }

        public static string GetDescriptionFrom(object obj)
        {
            if (IsDefinedOn(obj))
            {
                DescriptionAttribute attribute =
                    (DescriptionAttribute)
                    obj.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
                return attribute.Description;
            }
            return String.Empty;
        }

        public static string GetDescriptionFrom(PropertyInfo property)
        {
            if (IsDefinedOn(property))
            {
                DescriptionAttribute attribute =
                    (DescriptionAttribute)
                    property.GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
                return attribute.Description;
            }
            return String.Empty;
        }
    }
}
