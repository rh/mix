using System.Xml;
using System.Xml.XPath;
using Mix.Core;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;

namespace Mix.Tasks
{
    [Description("Adds a new attribute to the selected elements.")]
    public class AddAttribute : Task
    {
        [Option(SupportsXPathTemplates = true), Required]
        [Description("The name of the new attribute.")]
        public string Name { get; set; }

        [Option(SupportsXPathTemplates = true)]
        [Description("The value of the new attribute.")]
        public string Value { get; set; }

        [Option]
        [Description("An XPath expression, applied to the selected element, which determines before which attribute the new attribute is added.")]
        public string Before { get; set; }

        [Option]
        [Description("An XPath expression, applied to the selected element, which determines after which attribute the new attribute is added.")]
        public string After { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            if (!element.HasAttribute(Name))
            {
                var attribute = element.OwnerDocument.CreateAttribute(Name);
                attribute.Value = Value;

                if (!string.IsNullOrEmpty(After))
                {
                    try
                    {
                        var node = element.SelectSingleNode(After);
                        if (node != null && node is XmlAttribute)
                        {
                            element.Attributes.InsertAfter(attribute, node as XmlAttribute);
                            return;
                        }
                    }
                    catch (XPathException e)
                    {
                        var message = string.Format("'{0}' is not a valid XPath expression.", After);
                        throw new TaskExecutionException(message, e);
                    }
                }

                if (!string.IsNullOrEmpty(Before))
                {
                    try
                    {
                        var node = element.SelectSingleNode(Before);
                        if (node != null && node is XmlAttribute)
                        {
                            element.Attributes.InsertBefore(attribute, node as XmlAttribute);
                            return;
                        }
                    }
                    catch (XPathException e)
                    {
                        var message = string.Format("'{0}' is not a valid XPath expression.", Before);
                        throw new TaskExecutionException(message, e);
                    }
                }

                element.Attributes.Append(attribute);
            }
        }
    }
}