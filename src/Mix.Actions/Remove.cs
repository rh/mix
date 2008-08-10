using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Removes all selected elements, attributes, text nodes, CDATA sections, comments or processing instructions.")]
    public class Remove : Action
    {
        protected override void ExecuteCore(XmlElement element)
        {
            if (element == element.OwnerDocument.DocumentElement)
            {
                Context.Output.WriteLine("The document element cannot be removed.");
                return;
            }
            element.ParentNode.RemoveChild(element);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.OwnerElement.RemoveAttributeNode(attribute);
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.ParentNode.RemoveChild(text);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.ParentNode.RemoveChild(section);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.ParentNode.RemoveChild(comment);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            instruction.ParentNode.RemoveChild(instruction);
        }
    }
}