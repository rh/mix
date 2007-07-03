using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Replaces text in the text nodes of selected elements " +
                 "or in the values of selected attributes.")]
    public class ReplaceAction : Action
    {
        private string oldValue = String.Empty;
        private string newValue = String.Empty;

        [Argument, Required]
        [Description("The value to replace.")]
        public virtual string OldValue
        {
            get { return oldValue; }
            set { oldValue = value; }
        }

        [Argument]
        [Description("The value to replace the old value.")]
        public virtual string NewValue
        {
            get { return newValue; }
            set { newValue = value; }
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
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = attribute.Value.Replace(OldValue, NewValue);
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.Value = text.Value.Replace(OldValue, NewValue);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value = section.Value.Replace(OldValue, NewValue);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = comment.Value.Replace(OldValue, NewValue);
        }
    }
}