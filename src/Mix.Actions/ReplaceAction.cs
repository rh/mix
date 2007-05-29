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

        [Argument, Required]
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
                        node.Value = node.Value.Replace(OldValue, NewValue);
                    }
                    else if (node is XmlCDataSection)
                    {
                        node.Value = node.Value.Replace(OldValue, NewValue);
                    }
                }
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = attribute.Value.Replace(OldValue, NewValue);
        }
    }
}