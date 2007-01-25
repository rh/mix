using System;
using System.Diagnostics;
using System.Text;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Extracts the selected elements to new files.")]
    public class ExtractAction : Action
    {
        private string name;

        [Argument, Required]
        [Description("The name of the new file(s)." +
                     "\nPrepend with 'xpath:' to use an XPath expression on the current node.")]
        public string Name
        {
            [DebuggerStepThrough]
            get { return name; }
            [DebuggerStepThrough]
            set { name = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration =
                document.CreateXmlDeclaration("1.0", Encoding.UTF8.HeaderName, null);
            document.AppendChild(declaration);
            XmlNode root = document.ImportNode(element, true);
            document.AppendChild(root);

            string filename = GetFileName(element);
            document.Save(filename);
        }

        protected string GetFileName(XmlElement element)
        {
            if (name.StartsWith("xpath:"))
            {
                string xpath = name.Replace("xpath:", "");
                XmlNode node = element.SelectSingleNode(xpath);
                return String.Format("{0}.xml", node.Value);
            }
            else
            {
                return String.Format("{0}.xml", name);
            }
        }
    }
}
