using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Renames all selected elements or attributes.")]
    [ProcessingOrder(ProcessingOrder.Reverse)]
    public class Rename : Task
    {
        [Option, Required]
        [Description("The new name for the elements or attributes.")]
        public string Name { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            // TODO: what if element is DocumentElement?
            if (element.OwnerDocument.DocumentElement == element)
            {
                return;
            }

            var clone = element.OwnerDocument.CreateElement(Name);
            foreach (XmlAttribute attribute in element.Attributes)
            {
                clone.Attributes.Append(attribute.Clone() as XmlAttribute);
            }
            foreach (XmlNode child in element.ChildNodes)
            {
                clone.AppendChild(child.Clone());
            }
            element.ParentNode.ReplaceChild(clone, element);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            var owner = attribute.OwnerElement;

            if (owner.HasAttribute(Name))
            {
                return;
            }

            var clone = attribute.OwnerDocument.CreateAttribute(Name);
            clone.Value = attribute.Value;
            owner.Attributes.InsertBefore(clone, attribute);
            owner.Attributes.Remove(attribute);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            var parent = instruction.ParentNode;
            var clone = instruction.OwnerDocument.CreateProcessingInstruction(Name, instruction.Value);
            parent.ReplaceChild(clone, instruction);
        }
    }
}