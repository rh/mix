using System;
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
            get { return name; }
            set { name = value; }
        }

        [Argument]
        [Description("The value of the new attribute." +
                     "\nPrepend with 'xpath:' to use an XPath expression on the current node.")]
        public virtual string Value
        {
            get { return @value; }
            set { this.@value = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            XmlHelper.AddAttribute(element.OwnerDocument, element, Name, GetValue(element));
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            XmlHelper.AddAttribute(attribute.OwnerDocument, attribute.OwnerElement, Name, GetValue(attribute.OwnerElement));
        }

        private string GetValue(XmlNode element)
        {
            if (Value.StartsWith("xpath:"))
            {
                string xpath = Value.Replace("xpath:", "");
                XmlNode node = element.SelectSingleNode(xpath);
                return node.Value;
            }
            return Value;
        }
    }
}