using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;

namespace Mix.Tasks
{
    [Description("Converts the selected nodes to processing instructions.")]
    public class ConvertToProcessingInstruction : Task
    {
        [Argument]
        [Description("The name of the processing instruction. Required for nodes other then elements.")]
        public string Name { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            var instruction = element.OwnerDocument.CreateProcessingInstruction(element.Name, element.InnerXml);
            element.ParentNode.ReplaceChild(instruction, element);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            var instruction = attribute.OwnerDocument.CreateProcessingInstruction(attribute.Name, attribute.Value);
            attribute.OwnerElement.PrependChild(instruction);
            attribute.OwnerElement.Attributes.Remove(attribute);
        }

        protected override void ExecuteCore(XmlText text)
        {
            Validate();
            var instruction = text.OwnerDocument.CreateProcessingInstruction(Name, text.Value);
            text.ParentNode.ReplaceChild(instruction, text);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            Validate();
            var instruction = section.OwnerDocument.CreateProcessingInstruction(Name, section.Value);
            section.ParentNode.ReplaceChild(instruction, section);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            Validate();
            var instruction = comment.OwnerDocument.CreateProcessingInstruction(Name, comment.Value);
            comment.ParentNode.ReplaceChild(instruction, comment);
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                // TODO: remove duplication
                throw new RequirementException("'Name' is required when a node other then an element or attribute is selected.", "Name", "The name of the processing instruction. Required for nodes other then elements or attributes.");
            }
        }
    }
}