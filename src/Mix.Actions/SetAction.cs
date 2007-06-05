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
        public virtual string Text
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
                        element.FirstChild.Value = Text;
                    }
                    else if (element.FirstChild is XmlCDataSection)
                    {
                        element.FirstChild.Value = Text;
                    }
                }
                else
                {
                    element.InnerXml = String.Empty;
                    XmlText newElement = element.OwnerDocument.CreateTextNode(Text);
                    element.AppendChild(newElement);
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
            attribute.Value = Text;
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = Text;
        }
    }
}