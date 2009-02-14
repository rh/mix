using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Adds an XML fragment to the selected elements.")]
    public class AddFragment : Task
    {
        private string fragment = string.Empty;

        [Argument, XmlArgument, Required]
        [Description("The raw xml of the new element.")]
        public string Fragment
        {
            get { return fragment; }
            set { fragment = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            var child = element.OwnerDocument.CreateDocumentFragment();
            child.InnerXml = Fragment;
            element.AppendChild(child);
        }
    }
}