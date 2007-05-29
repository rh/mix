using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Renames all selected elements or attributes.")]
    public class RenameAction : Action
    {
        private string name = String.Empty;

        [Argument, Required]
        [Description("The new name for the elements or attributes.")]
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            // TODO: what if element is DocumentElement?
            if (element.OwnerDocument.DocumentElement == element)
            {
                return;
            }

            XmlElement newelement = element.OwnerDocument.CreateElement(Name);
            XmlHelper.CopyAttributes(element.OwnerDocument, element, newelement);
            XmlHelper.CopyChildNodes(element, newelement);
            XmlHelper.ReplaceElement(element, newelement);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            XmlElement owner = attribute.OwnerElement;

            if (owner.HasAttribute(Name))
            {
                return;
            }

            XmlAttribute newattribute =
                attribute.OwnerDocument.CreateAttribute(Name);
            newattribute.Value = attribute.Value;
            owner.Attributes.InsertBefore(newattribute, attribute);
            owner.Attributes.Remove(attribute);
        }
    }
}