using System;
using System.Text.RegularExpressions;
using System.Xml;
using Mix.Attributes;
using Mix.Exceptions;
using Mix.Extensions;

namespace Mix
{
    public static class TaskValidator
    {
        public static void Validate(Task task)
        {
            foreach (var property in task.GetType().GetProperties())
            {
                if (property.IsAnOption() && property.IsRequired())
                {
                    var value = property.GetValue(task, null);
                    if (value == null || value.ToString().Length == 0)
                    {
                        var message = string.Format("'{0}' is required.", property.Name.ToLower());
                        var description = "";
                        if (property.IsDefined(typeof(DescriptionAttribute), false))
                        {
                            var attribute = (DescriptionAttribute) property.GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
                            description = attribute.Description;
                        }
                        throw new RequirementException(message, property.Name, description);
                    }
                }

                if (XmlOptionAttribute.IsDefinedOn(property))
                {
                    var value = property.GetValue(task, null);
                    if (value != null && value.ToString().Trim().Length > 0)
                    {
                        var xml = value as string;
                        if (xml != null)
                        {
                            try
                            {
                                var document = new XmlDocument();
                                // Create a root node, because the XML is just a fragment.
                                document.LoadXml(string.Format("<root>{0}</root>", xml));
                            }
                            catch (XmlException e)
                            {
                                var message = string.Format("Option '{0}' ('{1}') is not valid XML: {2}", property.Name.ToLower(), xml, e.Message);
                                throw new XmlException(message, e);
                            }
                        }
                    }
                }

                if (RegexOptionAttribute.IsDefinedOn(property))
                {
                    var value = property.GetValue(task, null);
                    if (value != null && value.ToString().Trim().Length > 0)
                    {
                        var pattern = value as string;
                        if (pattern != null)
                        {
                            try
                            {
                                new Regex(pattern);
                            }
                            catch (ArgumentException e)
                            {
                                var message = string.Format("Option '{0}' is not a valid regular expression: {1}", property.Name.ToLower(), e.Message);
                                throw new ArgumentException(message, e);
                            }
                        }
                    }
                }
            }
        }
    }
}
