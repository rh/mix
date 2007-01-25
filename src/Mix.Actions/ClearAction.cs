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
    [Description("Removes all attributes and child elements of selected elements, " +
                 "or clears selected attributes.")]
    public sealed class ClearAction : Action
    {
        #region Action Overrides

        /// <summary>
        /// Removes all attributes and childnodes of <paramref name="element"/>.
        /// </summary>
        /// <param name="element"></param>
        protected override void ExecuteCore(XmlElement element)
        {
            element.RemoveAll();
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
