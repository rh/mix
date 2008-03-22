using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    /// <summary>
    /// Adds an <see cref="XmlComment"/> to the selected <see cref="XmlElement"/>(s),
    /// or adds the value of the new <see cref="XmlComment"/> to selected <see cref="XmlComment"/>(s).
    /// </summary>
    [Description("Adds a new comment.")]
    public class AddCommentAction : Action
    {
        private string @value = String.Empty;

        /// <summary>
        /// Gets or sets the value of the <see cref="XmlComment"/>.
        /// This argument is not required.
        /// If it is not set, an empty <see cref="XmlComment"/> will be added.
        /// </summary>
        [Argument]
        [Description("The value of the comment.")]
        public virtual string Value
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
            XmlComment comment = element.OwnerDocument.CreateComment(Value);
            element.AppendChild(comment);
        }

        /// <summary>
        /// Adds the value of the new <see cref="XmlComment"/> (e.g. <see cref="Value"/>) to the value of <paramref name="comment"/>.
        /// </summary>
        /// <param name="comment">
        /// The <see cref="XmlComment"/> to which <see cref="Value"/> should be added.
        /// </param>
        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value += Value;
        }
    }
}