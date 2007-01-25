using System;
using System.Diagnostics;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Replaces text in the text nodes of selected elements " +
                 "or in the values of selected attributes.")]
    public sealed class ReplaceAction : Action
    {
        #region Instance Variables

        private string oldValue = String.Empty;
        private string newValue = String.Empty;

        #endregion

        public ReplaceAction()
        {
        }

        public ReplaceAction(string oldValue, string newValue)
        {
            Check.ArgumentIsNotNullOrEmpty(oldValue, "oldValue");
            Check.ArgumentIsNotNullOrEmpty(newValue, "newValue");

            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        #region Properties

        [Argument, Required]
        [Description("The value to replace.")]
        public string OldValue
        {
            [DebuggerStepThrough]
            get { return oldValue; }
            [DebuggerStepThrough]
            set { oldValue = value; }
        }

        [Argument, Required]
        [Description("The value to replace the old value.")]
        public string NewValue
        {
            [DebuggerStepThrough]
            get { return newValue; }
            [DebuggerStepThrough]
            set { newValue = value; }
        }

        #endregion

        #region Action Overrides

        protected override void ExecuteCore(XmlElement element)
        {
            if (element.HasChildNodes)
            {
                if (element.FirstChild is XmlText)
                {
                    element.FirstChild.Value =
                        element.FirstChild.Value.Replace(OldValue, NewValue);
                }
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = attribute.Value.Replace(OldValue, NewValue);
        }

        #endregion
    }
}
