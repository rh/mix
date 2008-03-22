using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Sets the inner XML of the selected elements or comments.")]
    public class InnerXmlAction : Action
    {
        private string xml = String.Empty;

        [Argument, XmlArgument, Required]
        [Description("The literal XML of the selected elements.")]
        public string Xml
        {
            get { return xml; }
            set { xml = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            element.InnerXml = Xml;
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            ExecuteCore(attribute.OwnerElement);
        }

        protected override void ExecuteCore(XmlText text)
        {
            ExecuteCore(text.ParentNode as XmlElement);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            ExecuteCore(section.ParentNode as XmlElement);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            // InnerText is used because InnerXml will throw; an XmlComment
            // cannot have children.
            comment.InnerText = Xml;
        }
    }
}