using System;

namespace Mix.Core.Attributes
{
    /// <summary>
    /// An attribute to mark classes with a name.
    /// </summary>
    /// <example>
    /// In the following example, PrependAction is named 'prepend':
    /// <code>
    /// [Name("prepend")]
    /// public sealed class PrependAction : Action
    /// {
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class NameAttribute : Attribute
    {
        private readonly string name;

        public NameAttribute(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
        }

        public static bool IsDefinedOn(object obj)
        {
            return obj.GetType().IsDefined(typeof(NameAttribute), false);
        }

        public static string GetNameFrom(object obj)
        {
            if (IsDefinedOn(obj))
            {
                NameAttribute attribute =
                    (NameAttribute)
                    obj.GetType().GetCustomAttributes(typeof(NameAttribute), false)[0];
                return attribute.Name;
            }
            return String.Empty;
        }
    }
}
