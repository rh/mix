using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Sets the value of the selected elements, attributes, text nodes, CDATA sections, comments or processing instructions.")]
    public class Set : Mix.Core.Action
    {
        private string @value = String.Empty;

        [Argument]
        [Description("The value to set.")]
        public string Value
        {
            get { return @value; }
            set { this.@value = value; }
        }

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