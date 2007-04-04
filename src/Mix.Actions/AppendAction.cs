using System;
using System.Diagnostics;
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
            [DebuggerStepThrough]
            get { return text; }
            [DebuggerStepThrough]
            set { text = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            if (element.HasChildNodes)
            {
                if (element.FirstChild is XmlText)
                {
                    element.FirstChild.Value = element.FirstChild.Value + Text;
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
    }
}