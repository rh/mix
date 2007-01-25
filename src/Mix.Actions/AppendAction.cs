using System;
using System.Diagnostics;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Appends text to the text nodes of the selected elements, " +
                 "or to the value of the selected attributes.")]
    public sealed class AppendAction : Action
    {
        #region Instance Variables

        private string text = String.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppendAction"/> class.
        /// </summary>
        [DebuggerStepThrough]
        public AppendAction()
            : this(String.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppendAction"/> class.
        /// </summary>
        /// <param name="text">The text to append.</param>
        [DebuggerStepThrough]
        public AppendAction(string text)
        {
            this.text = text;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text to append.
        /// </summary>
        /// <value>The text to append.</value>
        [Argument, Required]
        [Description("The text to append.")]
        public string Text
        {
            [DebuggerStepThrough]
            get { return text; }
            [DebuggerStepThrough]
            set { text = value; }
        }

        #endregion

        #region Action Overrides

        /// <summary>
        ///
        /// </summary>
        /// <param name="element">The element.</param>
        protected override void ExecuteCore(XmlElement element)
        {
            if (element.HasChildNodes)
            {
                if (element.FirstChild is XmlText)
                {
                    element.FirstChild.Value = element.FirstChild.Value + Text;
                }
            }
            else
            {
                XmlText newElement = element.OwnerDocument.CreateTextNode(Text);
                element.AppendChild(newElement);
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = attribute.Value + Text;
        }

        #endregion
    }
}
