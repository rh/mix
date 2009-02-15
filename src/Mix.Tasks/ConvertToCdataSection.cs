using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Converts the selected nodes to CDATA sections.")]
    public class ConvertToCdataSection : Task
    {
        protected override void ExecuteCore(XmlElement element)
        {
            var section = element.OwnerDocument.CreateCDataSection(element.OuterXml);
            element.ParentNode.ReplaceChild(section, element);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            var data = String.Format("{0}=\"{1}\"", attribute.Name, attribute.Value);
            var section = attribute.OwnerDocument.CreateCDataSection(data);
            attribute.OwnerElement.PrependChild(section);
            attribute.OwnerElement.Attributes.Remove(attribute);
        }

        protected override void ExecuteCore(XmlText text)
        {
            var section = text.OwnerDocument.CreateCDataSection(text.Value);
            text.ParentNode.ReplaceChild(section, text);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            var section = comment.OwnerDocument.CreateCDataSection(comment.Value);
            comment.ParentNode.ReplaceChild(section, comment);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            var section = instruction.OwnerDocument.CreateCDataSection(instruction.Value);
            instruction.ParentNode.ReplaceChild(section, instruction);
        }
    }
}