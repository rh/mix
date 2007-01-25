using System;
using System.Diagnostics;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Copies the values of the selected attributes to new or existing attributes.")]
    public sealed class CopyAttributeAction : Action
    {
        #region Instance Variables

        private string name = String.Empty;

        #endregion

        #region Constructors

        [DebuggerStepThrough]
        public CopyAttributeAction()
            : this(String.Empty)
        {
        }

        [DebuggerStepThrough]
        public CopyAttributeAction(string name)
        {
            this.name = name;
        }

        #endregion

        #region Properties

        [Argument, Required]
        [Description("The name of the new or existing attribute.")]
        public string Name
        {
            [DebuggerStepThrough]
            get { return name; }
            [DebuggerStepThrough]
            set { name = value; }
        }

        #endregion

        #region Action Overrides

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            XmlElement owner = attribute.OwnerElement;

            if (owner.HasAttribute(Name))
            {
                owner.GetAttributeNode(Name).Value = attribute.Value;
            }
            else
            {
                XmlHelper.AddAttribute(attribute.OwnerDocument, owner, Name, attribute.Value);
            }
        }

        #endregion
    }
}
