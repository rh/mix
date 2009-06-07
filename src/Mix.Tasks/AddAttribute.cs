using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Adds a new attribute to the selected elements, or to the owner element of the selected attributes.")]
    public class AddAttribute : Task
    {
        [Argument, Required]
        [Description("The name of the new attribute.")]
        public string Name { get; set; }

        [Argument, Description("The value of the new attribute.\nPrepend with 'xpath:' to use an XPath expression on the selected element.")]
        public string Value { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            var value = GetValue(element);
            if (element.HasAttribute(Name))
            {
                return;
            }

            var attribute = element.OwnerDocument.CreateAttribute(Name);
            if (!string.IsNullOrEmpty(value))
            {
                attribute.Value = value;
            }
            element.Attributes.Append(attribute);
        }

        private string GetValue(XmlNode element)
        {
            if (Value != null && Value.StartsWith("xpath:"))
            {
                var xpath = Value.Replace("xpath:", "");
                var node = element.SelectSingleNode(xpath);
                return node.Value;
            }
            return Value;
        }
    }
}