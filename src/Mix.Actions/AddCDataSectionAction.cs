using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    /// <summary>
    /// Adds an <see cref="XmlCDataSection"/> to the selected <see cref="XmlElement"/>(s),
    /// or adds the value of the new <see cref="XmlCDataSection"/> to selected <see cref="XmlCDataSection"/>(s).
    /// </summary>
    [Description("Adds a new CDATA section.")]
    public class AddCDataSectionAction : Action
    {
        private string @value = String.Empty;

        /// <summary>
        /// Gets or sets the value of the <see cref="XmlCDataSection"/>.
        /// This argument is not required.
        /// If it is not set, an empty <see cref="XmlCDataSection"/> will be added.
        /// </summary>
        [Argument]
        [Description("The value of the CDATA section.")]
        public string Value
        {
            get { return @value; }
            set { this.@value = value; }
        }

        /// <summary>
        /// Adds an <see cref="XmlCDataSection"/> to <paramref name="element"/>.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XmlElement"/> to which an <see cref="XmlCDataSection"/> should be added.
        /// </param>
        protected override void ExecuteCore(XmlElement element)
        {
            XmlCDataSection section = element.OwnerDocument.CreateCDataSection(Value);
            element.AppendChild(section);
        }

        /// <summary>
        /// Adds the value of the new <see cref="XmlCDataSection"/> (e.g. <see cref="Value"/>) to the value of <paramref name="section"/>.
        /// </summary>
        /// <param name="section">
        /// The <see cref="XmlCDataSection"/> to which <see cref="Value"/> should be added.
        /// </param>
        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value += Value;
        }
    }
}