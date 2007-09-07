using System;
using System.Collections.Generic;
using System.Reflection;
using Mix.Core.Attributes;

namespace Mix.Core
{
    public class ArgumentInfo : IArgumentInfo
    {
        private string name = String.Empty;
        private string description = "[no description]";
        private bool required;

        public ArgumentInfo()
        {
        }

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }

        public bool Required
        {
            get { return required; }
        }

        public static IArgumentInfo[] For(object obj)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>();

            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                if (ArgumentAttribute.IsDefinedOn(property))
                {
                    properties.Add(property);
                }
            }

            IArgumentInfo[] arguments = new IArgumentInfo[properties.Count];

            if (properties.Count > 0)
            {
                for (int i = 0; i < properties.Count; i++)
                {
                    PropertyInfo property = properties[i];
                    ArgumentInfo argument = new ArgumentInfo();
                    argument.name = property.Name;
                    argument.required = RequiredAttribute.IsDefinedOn(property);
                    argument.description =
                        DescriptionAttribute.GetDescriptionFrom(property, "[no description]");
                    arguments[i] = argument;
                }
            }
            return arguments;
        }
    }
}