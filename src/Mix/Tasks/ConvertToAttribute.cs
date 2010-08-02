using System.Xml;
using Mix.Attributes;
using Mix.Exceptions;

namespace Mix.Tasks
{
    [Description("Converts the selected nodes to attributes.")]
    public class ConvertToAttribute : Task
    {
        [Option]
        [Description("The name of the attribute. Required for nodes other then elements or processing instructions.")]
        public string Name { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            var owner = element.ParentNode as XmlElement;
            var name = element.Name;

            if (owner != null && owner.Attributes[name] == null)
            {
                var xmlText = element.FirstChild as XmlText;
                if (xmlText != null)
                {
                    var attribute = element.OwnerDocument.CreateAttribute(name);
                    attribute.Value = xmlText.Value;
                    owner.Attributes.Append(attribute);
                    owner.RemoveChild(element);
                }
            }
        }

        protected override void ExecuteCore(XmlText text)
        {
            Validate();

            var element = text.ParentNode as XmlElement;

            if (element != null && element.Attributes[Name] == null)
            {
                var attribute = text.OwnerDocument.CreateAttribute(Name);
                attribute.Value = text.Value;
                element.Attributes.Append(attribute);
                element.RemoveChild(text);
            }
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            Validate();

            var element = section.ParentNode as XmlElement;

            if (element != null && element.Attributes[Name] == null)
            {
                var attribute = section.OwnerDocument.CreateAttribute(Name);
                attribute.Value = section.Value;
                element.Attributes.Append(attribute);
                element.RemoveChild(section);
            }
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            Validate();

            var element = comment.ParentNode as XmlElement;

            if (element != null && element.Attributes[Name] == null)
            {
                var attribute = comment.OwnerDocument.CreateAttribute(Name);
                attribute.Value = comment.Value;
                element.Attributes.Append(attribute);
                element.RemoveChild(comment);
            }
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            var element = instruction.ParentNode as XmlElement;
            var name = instruction.Name;

            if (element != null && element.Attributes[name] == null)
            {
                var attribute = instruction.OwnerDocument.CreateAttribute(name);
                attribute.Value = instruction.Value;
                element.Attributes.Append(attribute);
                element.RemoveChild(instruction);
            }
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                // TODO: remove duplication
                throw new RequirementException("'Name' is required when a node other then an element or a processing instruction is selected.", "Name", "The name of the processing instruction. Required for nodes other then elements or processing instructions.");
            }
        }
    }
}