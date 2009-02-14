using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Converts the selected nodes to comments.")]
    public class ConvertToComment : Task
    {
        protected override void ExecuteCore(XmlElement element)
        {
            var comment = element.OwnerDocument.CreateComment(element.OuterXml);
            element.ParentNode.ReplaceChild(comment, element);
        }

        protected override void ExecuteCore(XmlText text)
        {
            var comment = text.OwnerDocument.CreateComment(text.OuterXml);
            text.ParentNode.ReplaceChild(comment, text);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            var comment = section.OwnerDocument.CreateComment(section.OuterXml);
            section.ParentNode.ReplaceChild(comment, section);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            var comment = instruction.OwnerDocument.CreateComment(instruction.OuterXml);
            instruction.ParentNode.ReplaceChild(comment, instruction);
        }
    }
}