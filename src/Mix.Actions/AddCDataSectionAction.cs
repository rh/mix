using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Adds a new CDATA section.")]
    public class AddCDataSectionAction : Action
    {
        private string text = String.Empty;

        [Argument]
        [Description("The text of the CDATA section.")]
        public virtual string Text
        {
            get { return text; }
            set { text = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            XmlCDataSection section = element.OwnerDocument.CreateCDataSection(Text);
            element.AppendChild(section);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            ExecuteCore(attribute.OwnerElement);
        }

        protected override void ExecuteCore(XmlText text)
        {
            ExecuteCore(text.ParentNode as XmlElement);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value = section.Value + Text;
        }
    }
}