using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    /// <summary>
    /// Adds an <see cref="XmlDocumentFragment"/> to the selected elements.
    /// </summary>
    [Description("Adds an XML fragment to the selected elements.")]
    public class AddFragment : Task
    {
        private string fragment = string.Empty;

        /// <summary>
        /// Gets or sets the raw XML which will be added.
        /// This argument is required.
        /// This argument is validated as XML.
        /// </summary>
        [Argument, XmlArgument, Required]
        [Description("The raw xml of the new element.")]
        public string Fragment
        {
            get { return fragment; }
            set { fragment = value; }
        }

        /// <summary>
        /// Adds an <see cref="XmlDocumentFragment"/> to <paramref name="element"/>.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XmlElement"/> to which the <see cref="XmlDocumentFragment"/> should be added.
        /// </param>
        protected override void ExecuteCore(XmlElement element)
        {
            var child = element.OwnerDocument.CreateDocumentFragment();
            child.InnerXml = Fragment;
            element.AppendChild(child);
        }
    }
}