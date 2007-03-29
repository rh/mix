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
        #region Instance Variables

        private string xml = String.Empty;

        #endregion

        #region Constructors

        [DebuggerStepThrough]
        public InnerXmlAction()
            : this(String.Empty)
        {
        }

        [DebuggerStepThrough]
        public InnerXmlAction(string xml)
        {
            this.xml = xml;
        }

        #endregion

        #region Properties

        [Argument, Required]
        [Description("The literal XML of the selected elements.")]
        public virtual string Xml
        {
            [DebuggerStepThrough]
            get { return xml; }
            [DebuggerStepThrough]
            set { xml = value; }
        }

        #endregion

        #region Action Overrides

        protected override void ExecuteCore(XmlElement element)
        {
            if (element.NodeType == XmlNodeType.Element)
            {
                element.InnerXml = xml;
            }
        }

        #endregion
    }
}