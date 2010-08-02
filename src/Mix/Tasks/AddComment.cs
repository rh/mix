using System.Xml;
using Mix.Attributes;

namespace Mix.Tasks
{
    [Description("Adds a new comment.")]
    public class AddComment : AddNode
    {
        [Option(SupportsXPathTemplates = true)]
        [Description("The value of the comment.")]
        public string Value { get; set; }

        protected override XmlNode CreateNode(XmlElement element)
        {
            return element.OwnerDocument.CreateComment(Value);
        }
    }
}