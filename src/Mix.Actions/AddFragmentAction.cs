using System;
using System.Diagnostics;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Adds a new element containing the supplied fragment.")]
    public class AddFragmentAction : Action
    {
        private string fragment = String.Empty;

        [Argument, Required]
        [Description("The raw xml of the new element.")]
        public virtual string Fragment
        {
            [DebuggerStepThrough]
            get { return fragment; }
            [DebuggerStepThrough]
            set { fragment = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            XmlDocument document = element.OwnerDocument;
            XmlDocumentFragment child = document.CreateDocumentFragment();
            child.InnerXml = Fragment;
            element.AppendChild(child);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            ExecuteCore(attribute.OwnerElement);
        }
    }
}