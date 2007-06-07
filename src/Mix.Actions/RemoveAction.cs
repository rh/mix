using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Removes all selected elements or attributes.")]
    public class RemoveAction : Action
    {
        protected override void ExecuteCore(XmlElement element)
        {
            element.ParentNode.RemoveChild(element);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.OwnerElement.RemoveAttributeNode(attribute);
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.ParentNode.RemoveChild(text);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.ParentNode.RemoveChild(section);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.ParentNode.RemoveChild(comment);
        }
    }
}