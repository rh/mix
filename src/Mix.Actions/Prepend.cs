using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Prepends text to the text nodes of the selected elements, or to the value of the selected attributes.")]
    public class Prepend : Action
    {
        private string @value = String.Empty;

        [Argument, Required]
        [Description("The value to prepend.")]
        public string Value
        {
            get { return @value; }
            set { this.@value = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            if (element.HasChildNodes)
            {
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
            else
            {
                XmlText newElement = element.OwnerDocument.CreateTextNode(Value);
                element.AppendChild(newElement);
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = Value + attribute.Value;
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.Value = Value + text.Value;
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value = Value + section.Value;
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = Value + comment.Value;
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            instruction.Value = Value + instruction.Value;
        }
    }
}