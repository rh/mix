using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    /// <summary>
    /// Adds an <see cref="XmlAttribute"/> to the selected <see cref="XmlElement"/>(s).
    /// </summary>
    [Description("Adds a new attribute to the selected elements, " +
                 "or to the owner element of the selected attributes.")]
    public class AddAttributeAction : Action
    {
        private string name = String.Empty;
        private string @value = String.Empty;

        /// <summary>
        /// Gets or sets the name of the <see cref="XmlAttribute"/>.
        /// This is a required argument.
        /// </summary>
        [Argument, Required]
        [Description("The name of the new attribute.")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the value of the <see cref="XmlAttribute"/>.
        /// This argument is not required.
        /// If it is not set, an empty <see cref="XmlAttribute"/> will be added.
        /// </summary>
        [Argument]
        [Description("The value of the new attribute." +
                     "\nPrepend with 'xpath:' to use an XPath expression on the selected element.")]
        public string Value
        {
            get { return @value; }
            set { this.@value = value; }
        }

        /// <summary>
        /// Adds an <see cref="XmlAttribute"/> to <paramref name="element"/>.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XmlElement"/> to which an <see cref="XmlAttribute"/> should be added.
        /// </param>
        protected override void ExecuteCore(XmlElement element)
        {
            XmlHelper.AddAttribute(element.OwnerDocument, element, Name, GetValue(element));
        }

        private string GetValue(XmlNode element)
        {
            if (Value.StartsWith("xpath:"))
            {
                string xpath = Value.Replace("xpath:", "");
                XmlNode node = element.SelectSingleNode(xpath);
                return node.Value;
            }
            return Value;
        }
    }
}