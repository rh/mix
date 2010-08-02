using System.Xml;
using Mix.Attributes;

namespace Mix.Tasks
{
    [Description("Adds a new CDATA section.")]
    public class AddCdataSection : AddNode
    {
        [Option(SupportsXPathTemplates = true)]
        [Description("The value of the CDATA section.")]
        public string Value { get; set; }

        protected override XmlNode CreateNode(XmlElement element)
        {
            return element.OwnerDocument.CreateCDataSection(Value);
        }
    }
}