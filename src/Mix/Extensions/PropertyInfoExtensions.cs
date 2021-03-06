using System.Reflection;
using Mix.Attributes;

namespace Mix.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static bool IsAnOption(this PropertyInfo property)
        {
            return OptionAttribute.IsDefinedOn(property) || XmlOptionAttribute.IsDefinedOn(property) || RegexOptionAttribute.IsDefinedOn(property);
        }

        public static bool IsRequired(this PropertyInfo property)
        {
            return RequiredAttribute.IsDefinedOn(property);
        }
    }
}