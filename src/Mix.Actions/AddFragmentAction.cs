using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Adds a new element containing the supplied fragment.")]
    public class AddFragmentAction : Action
    {
        private string fragment = String.Empty;

        [Argument, XmlArgument, Required]
        [Description("The raw xml of the new element.")]
        public virtual string Fragment
        {
            get { return fragment; }
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