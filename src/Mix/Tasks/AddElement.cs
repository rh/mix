using System.Xml;
using Mix.Attributes;

namespace Mix.Tasks
{
    [Description("Adds a new element.")]
    public class AddElement : AddNode
    {
        [Option(SupportsXPathTemplates = true), Required]
        [Description("The name of the new element.")]
        public string Name { get; set; }

        [Option(SupportsXPathTemplates = true)]
        [Description("The value of the new element.")]
        public string Value { get; set; }

        protected override XmlNode CreateNode(XmlElement element)
        {
            var node = element.OwnerDocument.CreateElement(Name);
            node.InnerText = Value;
            return node;
        }
    }
}