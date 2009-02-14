using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Adds a new comment.")]
    public class AddComment : Task
    {
        [Argument, Description("The value of the comment.")]
        public string Value { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            var comment = element.OwnerDocument.CreateComment(Value);
            element.AppendChild(comment);
        }
    }
}