using System.Xml;
using Mix.Attributes;
using Mix.Exceptions;

namespace Mix.Tasks
{
    [Description("Converts the selected nodes to elements.")]
    public class ConvertToElement : Task
    {
        [Option]
        [Description("The name of the element. Required for nodes other then attributes or processing instructions.")]
        public string Name { get; set; }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            var owner = attribute.OwnerElement;
            var name = attribute.Name;
            var newElement = attribute.OwnerDocument.CreateElement(name);
            newElement.InnerText = attribute.Value;
            owner.InsertBefore(newElement, owner.FirstChild);
            owner.RemoveAttribute(name);
        }

        protected override void ExecuteCore(XmlText text)
        {
            Validate();
            var element = text.OwnerDocument.CreateElement(Name);
            element.InnerText = text.Value;
            text.ParentNode.ReplaceChild(element, text);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            Validate();
            var element = section.OwnerDocument.CreateElement(Name);
            element.InnerText = section.Value;
            section.ParentNode.ReplaceChild(element, section);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            if (comment.ParentNode is XmlDocument)
            {
                return;
            }

            Validate();
            var element = comment.OwnerDocument.CreateElement(Name);
            element.InnerText = comment.Value;
            comment.ParentNode.ReplaceChild(element, comment);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            if (instruction.ParentNode is XmlDocument)
            {
                return;
            }

            var element = instruction.OwnerDocument.CreateElement(instruction.Name);
            element.InnerText = instruction.Value;
            instruction.ParentNode.ReplaceChild(element, instruction);
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                // TODO: remove duplication
                throw new RequirementException("'Name' is required when a node other then an attribute or a processing instruction is selected.", "Name", "The name of the processing instruction. Required for nodes other then attributes or processing instructions.");
            }
        }
    }
}