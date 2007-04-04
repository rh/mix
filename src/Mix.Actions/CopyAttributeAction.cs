using System;
using System.Diagnostics;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Copies the values of the selected attributes to new or existing attributes.")]
    public class CopyAttributeAction : Action
    {
        private string name = String.Empty;

        [Argument, Required]
        [Description("The name of the new or existing attribute.")]
        public virtual string Name
        {
            [DebuggerStepThrough]
            get { return name; }
            [DebuggerStepThrough]
            set { name = value; }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            XmlElement owner = attribute.OwnerElement;

            if (owner.HasAttribute(Name))
            {
                owner.GetAttributeNode(Name).Value = attribute.Value;
            }
            else
            {
                XmlHelper.AddAttribute(attribute.OwnerDocument, owner, Name, attribute.Value);
            }
        }
    }
}