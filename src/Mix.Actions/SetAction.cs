using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Sets the value of the selected elements or attributes.")]
    public class SetAction : Action
    {
        private string text = String.Empty;

        [Argument]
        [Description("The text to set.")]
        public virtual string Value
        {
            get { return text; }
            set { text = value; }
        }

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
                else
                {
                    element.InnerXml = String.Empty;
                    XmlText newElement = element.OwnerDocument.CreateTextNode(Value);
                    element.AppendChild(newElement);
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
    }
}
