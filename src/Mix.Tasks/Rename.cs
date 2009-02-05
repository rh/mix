using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Renames all selected elements or attributes.")]
    [ProcessingOrder(ProcessingOrder.Reverse)]
    public class Rename : Task
    {
        private string name = string.Empty;

        [Argument, Required]
        [Description("The new name for the elements or attributes.")]
        public string Name
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

            var newelement = element.OwnerDocument.CreateElement(Name);
            XmlHelper.CopyAttributes(element.OwnerDocument, element, newelement);
            XmlHelper.CopyChildNodes(element, newelement);
            element.ParentNode.ReplaceChild(newelement, element);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            var owner = attribute.OwnerElement;

            if (owner.HasAttribute(Name))
            {
                return;
            }

            var newattribute = attribute.OwnerDocument.CreateAttribute(Name);
            newattribute.Value = attribute.Value;
            owner.Attributes.InsertBefore(newattribute, attribute);
            owner.Attributes.Remove(attribute);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            var parent = instruction.ParentNode;
            var newinstruction = instruction.OwnerDocument.CreateProcessingInstruction(Name, instruction.Value);
            parent.ReplaceChild(newinstruction, instruction);
        }
    }
}