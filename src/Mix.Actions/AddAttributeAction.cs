using System;
using System.Diagnostics;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Adds a new attribute to the selected elements, " +
                 "or to the owner element of the selected attributes.")]
    public class AddAttributeAction : Action
    {
        private string name = String.Empty;
        private string @value = String.Empty;

        [Argument, Required]
        [Description("The name of the new attribute.")]
        public virtual string Name
        {
            [DebuggerStepThrough]
            get { return name; }
            [DebuggerStepThrough]
            set { name = value; }
        }

        [Argument]
        [Description("The value of the new attribute.")]
        public virtual string Value
        {
            [DebuggerStepThrough]
            get { return @value; }
            [DebuggerStepThrough]
            set { this.@value = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            XmlHelper.AddAttribute(element.OwnerDocument, element, Name, Value);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            XmlHelper.AddAttribute(attribute.OwnerDocument, attribute.OwnerElement, Name, Value);
        }
    }
}