using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Adds a new CDATA section.")]
    public class AddCdataSection : Task
    {
        [Option(SupportsXPathTemplates = true)]
        [Description("The value of the CDATA section.")]
        public string Value { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            var section = element.OwnerDocument.CreateCDataSection(Value);
            element.AppendChild(section);
        }
    }
}