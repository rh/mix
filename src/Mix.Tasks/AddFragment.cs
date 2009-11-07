using System.Xml;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Adds an XML fragment to the selected elements.")]
    public class AddFragment : AddNode
    {
        [XmlOption, Required]
        [Description("The raw XML of the new element.")]
        public string Fragment { get; set; }

        protected override XmlNode CreateNode(XmlElement element)
        {
            var node = element.OwnerDocument.CreateDocumentFragment();
            node.InnerXml = Fragment;
            return node;
        }
    }
}