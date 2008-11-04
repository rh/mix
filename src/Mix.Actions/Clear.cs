using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Clears the text nodes of elements, or the value of attributes, CDATA sections, comments or processing instructions.")]
    public class Clear : Mix.Core.Action
    {
        protected override void ExecuteCore(XmlElement element)
        {
            Recurse(element);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = String.Empty;
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.Value = String.Empty;
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value = String.Empty;
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = String.Empty;
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            instruction.Value = String.Empty;
        }
    }
}