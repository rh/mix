using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    /// <summary>
    /// Adds an <see cref="XmlCDataSection"/> to the selected <see cref="XmlElement"/>(s).
    /// </summary>
    [Description("Adds a new CDATA section.")]
    public class AddCdataSection : Task
    {
        private string @value = string.Empty;

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
            var section = element.OwnerDocument.CreateCDataSection(Value);
            element.AppendChild(section);
        }
    }
}