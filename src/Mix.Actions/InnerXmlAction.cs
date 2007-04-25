using System;
using System.Diagnostics;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Sets the inner XML of the selected elements.")]
    public class InnerXmlAction : Action
    {
        private string xml = String.Empty;

        [Argument, Required]
        [Description("The literal XML of the selected elements.")]
        public virtual string Xml
        {
            [DebuggerStepThrough]
            get { return xml; }
            [DebuggerStepThrough]
            set { xml = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            if (element.NodeType == XmlNodeType.Element)
            {
                element.InnerXml = xml;
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            ExecuteCore(attribute.OwnerElement);
        }
    }
}