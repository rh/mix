using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    /// <summary>
    /// Adds a <see cref="XmlProcessingInstruction"/> to selected elements.
    /// The name of the <see cref="XmlProcessingInstruction"/> will be <see cref="Name"/>,
    /// the value will be <see cref="Value"/>.
    /// </summary>
    [Description("Adds a new processing instruction.")]
    public class AddProcessingInstruction : Task
    {
        private string name = string.Empty;
        private string @value = string.Empty;

        /// <summary>
        /// Gets or sets the name of the <see cref="XmlProcessingInstruction"/>.
        /// This is a required argument.
        /// </summary>
        [Argument, Required]
        [Description("The name of the processing instruction.")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the value of the <see cref="XmlProcessingInstruction"/>.
        /// This argument is not required.
        /// If it is not set, an empty <see cref="XmlProcessingInstruction"/> will be added.
        /// </summary>
        [Argument]
        [Description("The value of the processing instruction.")]
        public string Value
        {
            get { return @value; }
            set { this.@value = value; }
        }

        /// <summary>
        /// Adds an <see cref="XmlProcessingInstruction"/> to <paramref name="element"/>.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XmlElement"/> to which the <see cref="XmlProcessingInstruction"/> should be added.
        /// </param>
        protected override void ExecuteCore(XmlElement element)
        {
            var instruction = element.OwnerDocument.CreateProcessingInstruction(Name, Value);
            element.AppendChild(instruction);
        }
    }
}