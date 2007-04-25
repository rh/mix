using System;
using System.Diagnostics;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Adds a new root element.")]
    public class AddRootElementAction : Action
    {
        private string name = String.Empty;

        [Argument, Required]
        [Description("The name of the new root element.")]
        public virtual string Name
        {
            [DebuggerStepThrough]
            get { return name; }
            [DebuggerStepThrough]
            set { name = value; }
        }

        protected override bool ExecuteCore(IContext context)
        {
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement(Name);
            root.InnerXml = XmlHelper.RemoveXmlDeclaration(context.Xml);
            document.AppendChild(root);
            context.Xml = document.InnerXml;
            return true;
        }
    }
}