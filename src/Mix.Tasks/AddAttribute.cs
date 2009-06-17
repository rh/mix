using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Adds a new attribute to the selected elements.")]
    public class AddAttribute : Task
    {
        [Option(SupportsXPathTemplates = true), Required]
        [Description("The name of the new attribute.")]
        public string Name { get; set; }

        [Option(SupportsXPathTemplates = true), Description("The value of the new attribute.")]
        public string Value { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            if (!element.HasAttribute(Name))
            {
                var attribute = element.OwnerDocument.CreateAttribute(Name);
                attribute.Value = Value;
                element.Attributes.Append(attribute);
            }
        }
    }
}