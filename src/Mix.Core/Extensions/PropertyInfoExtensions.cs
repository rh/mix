using System.Reflection;
using Mix.Core.Attributes;

namespace Mix.Core.Extensions
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