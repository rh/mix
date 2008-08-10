using System;
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
    public class Extract : Action
    {
        private string name = String.Empty;
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
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, null);
            document.AppendChild(declaration);
            XmlNode root = document.ImportNode(element, true);
            document.AppendChild(root);

            string filename = GetFileName(element);

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
            Debug.Assert(!String.IsNullOrEmpty(Name), "!String.IsNullOrEmpty(Name)");

            if (Name.StartsWith("xpath:"))
            {
                string xpath = Name.Replace("xpath:", "");
                XmlNode node = element.SelectSingleNode(xpath);
                return String.Format("{0}.xml", node.Value);
            }
            else
            {
                return String.Format("{0}.xml", Name);
            }
        }
    }
}