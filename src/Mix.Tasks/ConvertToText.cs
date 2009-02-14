using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Converts the selected nodes to text.")]
    public class ConvertToText : Task
    {
        protected override void ExecuteCore(XmlElement element)
        {
            var text = element.OwnerDocument.CreateTextNode(element.OuterXml);
            element.ParentNode.ReplaceChild(text, element);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            var text = section.OwnerDocument.CreateTextNode(section.Value);
            section.ParentNode.ReplaceChild(text, section);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            var text = comment.OwnerDocument.CreateTextNode(comment.Value);
            comment.ParentNode.ReplaceChild(text, comment);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            var text = instruction.OwnerDocument.CreateTextNode(instruction.Value);
            instruction.ParentNode.ReplaceChild(text, instruction);
        }
    }
}