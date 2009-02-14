using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Appends text to selected nodes (if they 'behave' like text), or to the value of the selected attributes.")]
    public class Append : Task
    {
        [Argument, Required]
        [Description("The value to append.")]
        public string Value { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            if (element.HasChildNodes)
            {
                if (element.ChildNodes.Count == 1)
                {
                    if (element.FirstChild is XmlText)
                    {
                        ExecuteCore(element.FirstChild as XmlText);
                    }
                    else if (element.FirstChild is XmlCDataSection)
                    {
                        ExecuteCore(element.FirstChild as XmlCDataSection);
                    }
                }
            }
            else
            {
                var newElement = element.OwnerDocument.CreateTextNode(Value);
                element.AppendChild(newElement);
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value += Value;
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.Value += Value;
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value += Value;
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value += Value;
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            instruction.Value += Value;
        }
    }
}