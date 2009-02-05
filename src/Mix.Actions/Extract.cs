using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Extracts the selected elements to new files.")]
    [ProcessingOrder(ProcessingOrder.Reverse)]
    public class Extract : Task
    {
        private string name = string.Empty;
        private readonly TextWriter writer;

        public Extract()
        {
        }

        public Extract(TextWriter writer)
        {
            this.writer = writer;
        }

        [Argument, Required]
        [Description("The name of the new file(s).\nPrepend with 'xpath:' to use an XPath expression on the current node.")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            var document = new XmlDocument();
            var declaration = document.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, null);
            document.AppendChild(declaration);
            var root = document.ImportNode(element, true);
            document.AppendChild(root);

            var filename = GetFileName(element);

            if (writer == null)
            {
                document.Save(filename);
            }
            else
            {
                document.Save(writer);
            }
        }

        protected virtual string GetFileName(XmlElement element)
        {
            Debug.Assert(!string.IsNullOrEmpty(Name), "!String.IsNullOrEmpty(Name)");

            if (Name.StartsWith("xpath:"))
            {
                var xpath = Name.Replace("xpath:", "");
                var node = element.SelectSingleNode(xpath);
                return string.Format("{0}.xml", node.Value);
            }
            return string.Format("{0}.xml", Name);
        }
    }
}