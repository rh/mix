using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Adds a new attribute to the selected elements, or to the owner element of the selected attributes.")]
    public class AddAttribute : Task
    {
        private string name = string.Empty;
        private string @value = string.Empty;

        [Argument, Required]
        [Description("The name of the new attribute.")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Argument]
        [Description("The value of the new attribute.\nPrepend with 'xpath:' to use an XPath expression on the selected element.")]
        public string Value
        {
            get { return @value; }
            set { this.@value = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            XmlHelper.AddAttribute(element.OwnerDocument, element, Name, GetValue(element));
        }

        private string GetValue(XmlNode element)
        {
            if (Value.StartsWith("xpath:"))
            {
                var xpath = Value.Replace("xpath:", "");
                var node = element.SelectSingleNode(xpath);
                return node.Value;
            }
            return Value;
        }
    }
}