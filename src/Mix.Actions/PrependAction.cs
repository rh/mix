using System;
using System.Diagnostics;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Prepends text to the text nodes of the selected elements," +
                 "or to the value of the selected attributes.")]
    public class PrependAction : Action
    {
        #region Instance Variables

        private string text = String.Empty;

        #endregion

        #region Constructors

        [DebuggerStepThrough]
        public PrependAction()
            : this(String.Empty)
        {
        }

        [DebuggerStepThrough]
        public PrependAction(string text)
        {
            this.text = text;
        }

        #endregion

        #region Properties

        [Argument, Required]
        [Description("The text to prepend.")]
        public virtual string Text
        {
            [DebuggerStepThrough]
            get { return text; }
            [DebuggerStepThrough]
            set { text = value; }
        }

        #endregion

        #region Action Overrides

        protected override void ExecuteCore(XmlElement element)
        {
            if (element.HasChildNodes)
            {
                if (element.FirstChild is XmlText)
                {
                    element.FirstChild.Value = Text + element.FirstChild.Value;
                }
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = Text + attribute.Value;
        }

        #endregion
    }
}