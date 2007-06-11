using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Adds a new CDATA section.")]
    public class AddCDataSectionAction : Action
    {
        private string @value = String.Empty;

        [Argument]
        [Description("The value of the CDATA section.")]
        public virtual string Value
        {
            get { return @value; }
            set { this.@value = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            XmlCDataSection section = element.OwnerDocument.CreateCDataSection(Value);
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
            section.Value = section.Value + Value;
        }
    }
}