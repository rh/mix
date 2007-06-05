using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Prepends text to the text nodes of the selected elements, " +
                 "or to the value of the selected attributes.")]
    public class PrependAction : Action
    {
        private string text = String.Empty;

        [Argument, Required]
        [Description("The text to prepend.")]
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
                        node.Value = Text + node.Value;
                    }
                    else if (node is XmlCDataSection)
                    {
                        node.Value = Text + node.Value;
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
            attribute.Value = Text + attribute.Value;
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = Text + comment.Value;
        }
    }
}