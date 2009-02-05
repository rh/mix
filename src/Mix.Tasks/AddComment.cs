using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    /// <summary>
    /// Adds an <see cref="XmlComment"/> to the selected <see cref="XmlElement"/>(s).
    /// </summary>
    [Description("Adds a new comment.")]
    public class AddComment : Task
    {
        private string @value = string.Empty;

        /// <summary>
        /// Gets or sets the value of the <see cref="XmlComment"/>.
        /// This argument is not required.
        /// If it is not set, an empty <see cref="XmlComment"/> will be added.
        /// </summary>
        [Argument]
        [Description("The value of the comment.")]
        public string Value
        {
            get { return @value; }
            set { this.@value = value; }
        }

        /// <summary>
        /// Adds an <see cref="XmlComment"/> to <paramref name="element"/>.
        /// </summary>
        /// <param name="element">
        /// The <see cref="XmlElement"/> to which an <see cref="XmlComment"/> should be added.
        /// </param>
        protected override void ExecuteCore(XmlElement element)
        {
            var comment = element.OwnerDocument.CreateComment(Value);
            element.AppendChild(comment);
        }
    }
}