using System.Xml;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Adds a new processing instruction.")]
    public class AddPi : AddNode
    {
        [Option(SupportsXPathTemplates = true), Required]
        [Description("The name of the processing instruction.")]
        public string Name { get; set; }

        [Option(SupportsXPathTemplates = true)]
        [Description("The value of the processing instruction.")]
        public string Value { get; set; }

        protected override XmlNode CreateNode(XmlElement element)
        {
            return element.OwnerDocument.CreateProcessingInstruction(Name, Value);
        }
    }
}