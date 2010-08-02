using System.Xml;
using Mix.Attributes;

namespace Mix.Tasks
{
    [Description("Sets the value of the selected elements, attributes, text nodes, CDATA sections, comments or processing instructions.")]
    public class Set : Task
    {
        [Option(SupportsXPathTemplates = true)]
        [Description("The value to set.")]
        public string Value { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            element.InnerXml = Value;
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = Value;
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.Value = Value;
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value = Value;
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = Value;
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            instruction.Value = Value;
        }
    }
}