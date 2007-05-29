using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Adds a new element.")]
    public class AddElementAction : Action
    {
        private string name = String.Empty;

        [Argument, Required]
        [Description("The name of the new element.")]
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            XmlElement child = element.OwnerDocument.CreateElement(Name);
            element.AppendChild(child);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            ExecuteCore(attribute.OwnerElement);
        }
    }
}