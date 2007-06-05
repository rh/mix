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

        [Argument, Required]
        [Description("The literal XML of the selected elements.")]
        public virtual string Xml
        {
            get { return xml; }
            set { xml = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            if (element.NodeType == XmlNodeType.Element)
            {
                element.InnerXml = Xml;
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            ExecuteCore(attribute.OwnerElement);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            // InnerText is used because InnerXml will throw; an XmlComment
            // cannaot have children.
            comment.InnerText = Xml;
        }
    }
}