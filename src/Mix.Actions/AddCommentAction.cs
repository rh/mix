using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Adds a new comment.")]
    public class AddCommentAction : Action
    {
        private string @value = String.Empty;

        [Argument]
        [Description("The value of the comment.")]
        public virtual string Value
        {
            get { return @value; }
            set { this.@value = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            XmlComment comment = element.OwnerDocument.CreateComment(Value);
            element.AppendChild(comment);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            ExecuteCore(attribute.OwnerElement);
        }

        protected override void ExecuteCore(XmlText text)
        {
            ExecuteCore(text.ParentNode as XmlElement);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = comment.Value + Value;
        }
    }
}