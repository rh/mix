using System.Collections.Generic;
using System.Reflection;
using Mix.Core.Attributes;
using Mix.Core.Extensions;

namespace Mix.Core
{
    public class OptionInfo
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Required { get; private set; }

        public static OptionInfo[] For(object obj)
        {
            var properties = new List<PropertyInfo>();

            foreach (var property in obj.GetType().GetProperties())
            {
                if (property.IsAnOption())
                {
                    properties.Add(property);
                }
            }

            var options = new OptionInfo[properties.Count];

            if (properties.Count > 0)
            {
                for (var i = 0; i < properties.Count; i++)
                {
                    var property = properties[i];
                    var option = new OptionInfo {Name = property.Name, Required = property.IsRequired(), Description = DescriptionAttribute.GetDescriptionFrom(property, "[no description]")};
                    options[i] = option;
                }
            }
            return options;
        }
    }
}