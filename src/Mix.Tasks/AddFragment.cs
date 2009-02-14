using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Adds an XML fragment to the selected elements.")]
    public class AddFragment : Task
    {
        [Argument, XmlArgument, Required]
        [Description("The raw xml of the new element.")]
        public string Fragment { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            var child = element.OwnerDocument.CreateDocumentFragment();
            child.InnerXml = Fragment;
            element.AppendChild(child);
        }
    }
}