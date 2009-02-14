using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Adds a new comment.")]
    public class AddComment : Task
    {
        private string @value = string.Empty;

        [Argument]
        [Description("The value of the comment.")]
        public string Value
        {
            get { return @value; }
            set { this.@value = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            var comment = element.OwnerDocument.CreateComment(Value);
            element.AppendChild(comment);
        }
    }
}