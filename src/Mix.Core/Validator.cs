using System;
using System.Reflection;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;

namespace Mix.Core
{
    public class Validator
    {
        public readonly object obj;

        public Validator(object obj)
        {
            Check.ArgumentIsNotNull(obj, "obj");
            this.obj = obj;
        }

        public void Validate()
        {
            foreach (PropertyInfo property in obj.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty))
            {
                if (RequiredAttribute.IsDefinedOn(property))
                {
                    object value = property.GetValue(obj, null);
                    if (value == null || value.ToString().Trim().Length == 0)
                    {
                        ThrowRequirementException(property);
                    }
                }
            }
        }

        private void ThrowRequirementException(PropertyInfo property)
        {
            string name = property.Name;
            string message = String.Format("A value for '{0}' is required.", name);
            string description = DescriptionAttribute.GetDescriptionFrom(obj);
            throw new RequirementException(message, name, description);
        }
    }
}