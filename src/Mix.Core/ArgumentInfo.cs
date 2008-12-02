using System.Collections.Generic;
using System.Reflection;
using Mix.Core.Attributes;

namespace Mix.Core
{
    public class ArgumentInfo : IArgumentInfo
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Required { get; private set; }

        public static IArgumentInfo[] For(object obj)
        {
            var properties = new List<PropertyInfo>();

            foreach (var property in obj.GetType().GetProperties())
            {
                if (ArgumentAttribute.IsDefinedOn(property))
                {
                    properties.Add(property);
                }
            }

            var arguments = new IArgumentInfo[properties.Count];

            if (properties.Count > 0)
            {
                for (var i = 0; i < properties.Count; i++)
                {
                    var property = properties[i];
                    var argument = new ArgumentInfo {Name = property.Name, Required = RequiredAttribute.IsDefinedOn(property), Description = DescriptionAttribute.GetDescriptionFrom(property, "[no description]")};
                    arguments[i] = argument;
                }
            }
            return arguments;
        }
    }
}