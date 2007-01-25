using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Removes all selected elements or attributes.")]
    public sealed class RemoveAction : Action
    {
        #region Action Overrides

        protected override void ExecuteCore(XmlElement element)
        {
            element.ParentNode.RemoveChild(element);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.OwnerElement.RemoveAttributeNode(attribute);
        }

        #endregion
    }
}
