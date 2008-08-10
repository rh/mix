using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    /// <summary>
    /// Adds an <see cref="XmlElement"/> to the selected <see cref="XmlElement"/>(s).
    /// </summary>
    [Description("Adds a new element.")]
    public class AddElement : Action
    {
        private string name = String.Empty;
        private string @value = String.Empty;

        /// <summary>
        /// Gets or sets the name of the <see cref="XmlElement"/>.
        /// This argument is required.
        /// </summary>
        [Argument, Required]
        [Description("The name of the new element.")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the value of the <see cref="XmlElement"/>.
        /// This argument is not required.
        /// If it is not set, an empty <see cref="XmlElement"/> will be added.
        /// </summary>
        [Argument]
        [Description("The value of the new element.")]
        public string Value
        {
            get { return @value; }
            set { this.@value = value; }
        }

        /// <summary>
        /// Adds an <see cref="XmlElement"/> to <paramref name="element"/>.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XmlElement"/> to which an <see cref="XmlElement"/> should be added.
        /// </param>
        protected override void ExecuteCore(XmlElement element)
        {
            XmlElement child = element.OwnerDocument.CreateElement(Name);
            child.InnerText = Value;
            element.AppendChild(child);
        }
    }
}