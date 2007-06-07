using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Appends text to the text nodes of the selected elements, " +
                 "or to the value of the selected attributes.")]
    public class AppendAction : Action
    {
        private string text = String.Empty;

        [Argument, Required]
        [Description("The text to append.")]
        public virtual string Text
        {
            get { return text; }
            set { text = value; }
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
                }
            }
            else
            {
                XmlText newElement = element.OwnerDocument.CreateTextNode(Text);
                element.AppendChild(newElement);
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = attribute.Value + Text;
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.Value = text.Value + Text;
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value = section.Value + Text;
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = comment.Value + Text;
        }
    }
}