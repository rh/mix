using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Trims the text nodes of the selected elements, or the value of the selected attributes, text nodes, CDATA sections, comments or processing instructions.")]
    public class TrimAction : Action
    {
        protected override void ExecuteCore(XmlElement element)
        {
            Recurse(element);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = attribute.Value.Trim();
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.Value = text.Value.Trim();
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value = section.Value.Trim();
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = comment.Value.Trim();
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            instruction.Value = instruction.Value.Trim();
        }
    }
}