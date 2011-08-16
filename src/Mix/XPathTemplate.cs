using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using Mix.Exceptions;
using Mix.Extensions;

namespace Mix
{
    public static class XPathTemplate
    {
        public static void EvaluateProperties(XmlNode node, object target, Dictionary<PropertyInfo, string> properties)
        {
            foreach (var property in properties.Keys)
            {
                var value = Evaluate(node, properties[property]);
                if (string.IsNullOrEmpty(value) && property.IsRequired())
                {
                    throw new XPathTemplateException("", property.Name, properties[property]);
                }
                property.SetValue(target, value, null);
            }
        }

        public static string Evaluate(XmlNode context, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            // Because the MatchEvaluator needs to access the context node, we have to use a delegate
            var template = new Regex("{([^0-9,}]{1}[^}]+)}");

            return template.Replace(value,
                                    match =>
                                        {
                                            // Strip the template characters, e.g. {foo} > foo
                                            var xpath = match.Value.Substring(1, match.Value.Length - 2);
                                            try
                                            {
                                                var node = context.SelectSingleNode(xpath);
                                                if (node != null)
                                                {
                                                    return node.InnerText;
                                                }
                                            }
                                            catch (XPathException)
                                            {
                                                return string.Empty;
                                            }
                                            return string.Empty;
                                        });
        }
    }
}