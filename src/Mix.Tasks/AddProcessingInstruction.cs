using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Adds a new processing instruction.")]
    public class AddProcessingInstruction : Task
    {
        [Argument, Required]
        [Description("The name of the processing instruction.")]
        public string Name { get; set; }

        [Argument]
        [Description("The value of the processing instruction.")]
        public string Value { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            var instruction = element.OwnerDocument.CreateProcessingInstruction(Name, Value);
            element.AppendChild(instruction);
        }
    }
}