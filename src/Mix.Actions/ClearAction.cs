using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    /// <summary>
    /// Clears the selected element or attribute.
    /// </summary>
    /// <example>
    /// TODO: add example
    /// </example>
    [Description("Clears the text nodes of selected elements, " +
                 "or the value of selected attributes.")]
    public class ClearAction : Action
    {
        #region Action Overrides

        /// <summary>
        /// Removes all attributes and childnodes of <paramref name="element"/>.
        /// </summary>
        /// <param name="element"></param>
        protected override void ExecuteCore(XmlElement element)
        {
            if (element.HasChildNodes)
            {
                if (element.FirstChild is XmlText)
                {
                    element.FirstChild.Value = String.Empty;
                }
                else if (element.FirstChild is XmlCDataSection)
                {
                    element.FirstChild.Value = String.Empty;
                }
            }
        }

        /// <summary>
        /// Clears the value of <paramref name="attribute"/>.
        /// </summary>
        /// <param name="attribute"></param>
        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = String.Empty;
        }

        #endregion
    }
}