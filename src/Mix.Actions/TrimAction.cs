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
            foreach (XmlAttribute attribute in element.Attributes)
            {
                ExecuteCore(attribute);
            }

            foreach (XmlNode node in element.ChildNodes)
            {
                if (node is XmlText)
                {
                    ExecuteCore(node as XmlText);
                }
                else if (node is XmlCDataSection)
                {
                    ExecuteCore(node as XmlCDataSection);
                }
                else if (node is XmlComment)
                {
                    ExecuteCore(node as XmlComment);
                }
                else if (node is XmlProcessingInstruction)
                {
                    ExecuteCore(node as XmlProcessingInstruction);
                }
            }
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