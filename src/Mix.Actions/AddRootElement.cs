using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Adds a new root element.")]
    public class AddRootElement : Mix.Core.Action
    {
        private string name = String.Empty;

        [Argument, Required]
        [Description("The name of the new root element.")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        protected override bool ExecuteCore(IContext context)
        {
            XmlDocument document = new XmlDocument();
            XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            document.AppendChild(declaration);
            XmlElement root = document.CreateElement(Name);
            root.InnerXml = XmlHelper.RemoveXmlDeclaration(context.Xml);
            document.AppendChild(root);
            context.Xml = document.OuterXml;
            return true;
        }
    }
}