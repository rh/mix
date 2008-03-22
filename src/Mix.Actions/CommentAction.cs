using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Comments the selected elements.")]
    public class CommentAction : Action
    {
        protected override void ExecuteCore(XmlElement element)
        {
            XmlComment comment = element.OwnerDocument.CreateComment(element.OuterXml);
            element.ParentNode.InsertBefore(comment, element);
            element.ParentNode.RemoveChild(element);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            ExecuteCore(attribute.OwnerElement);
        }

        protected override void ExecuteCore(XmlText text)
        {
            XmlComment comment = text.OwnerDocument.CreateComment(text.OuterXml);
            text.ParentNode.InsertBefore(comment, text);
            text.ParentNode.RemoveChild(text);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            XmlComment comment = section.OwnerDocument.CreateComment(section.OuterXml);
            section.ParentNode.InsertBefore(comment, section);
            section.ParentNode.RemoveChild(section);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            XmlComment comment = instruction.OwnerDocument.CreateComment(instruction.OuterXml);
            instruction.ParentNode.ReplaceChild(comment, instruction);
        }
    }
}