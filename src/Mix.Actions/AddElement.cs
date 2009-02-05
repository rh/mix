using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    /// <summary>
    /// Adds an <see cref="XmlElement"/> to the selected <see cref="XmlElement"/>(s).
    /// </summary>
    [Description("Adds a new element.")]
    public class AddElement : Task
    {
        private string name = string.Empty;
        private string @value = string.Empty;
        private string before = string.Empty;
        private string after = string.Empty;

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

        [Argument]
        [Description("An XPath expression, applied to the selected element, which determines before which child the new element is added.")]
        public string Before
        {
            get { return before; }
            set { before = value; }
        }

        [Argument]
        [Description("An XPath expression, applied to the selected element, which determines after which child the new element is added.")]
        public string After
        {
            get { return after; }
            set { after = value; }
        }

        /// <summary>
        /// Adds an <see cref="XmlElement"/> to <paramref name="element"/>.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XmlElement"/> to which an <see cref="XmlElement"/> should be added.
        /// </param>
        protected override void ExecuteCore(XmlElement element)
        {
            var child = element.OwnerDocument.CreateElement(Name);
            child.InnerText = Value;

            if (!string.IsNullOrEmpty(After))
            {
                var node = element.SelectSingleNode(After);
                if (node != null)
                {
                    element.InsertAfter(child, node);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(Before))
            {
                var node = element.SelectSingleNode(Before);
                if (node != null)
                {
                    element.InsertBefore(child, node);
                    return;
                }
            }

            element.AppendChild(child);
        }
    }
}