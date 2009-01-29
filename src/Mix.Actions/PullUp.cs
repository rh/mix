using System.Xml;
using Mix.Core;

namespace Mix.Actions
{
    public class PullUp : Action
    {
        protected override void ExecuteCore(XmlElement element)
        {
            if (element == element.OwnerDocument.DocumentElement)
            {
                Context.Output.WriteLine("The document element cannot be pulled-up.");
                return;
            }

            var parent = element.ParentNode as XmlElement;

            if (parent == element.OwnerDocument.DocumentElement)
            {
                Context.Output.WriteLine("A child element of the document element cannot be pulled-up.");
                return;
            }

            var clone = element.CloneNode(true);
            parent.ParentNode.InsertBefore(clone, parent);
            parent.RemoveChild(element);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            var parent = attribute.OwnerElement;

            if (parent == attribute.OwnerDocument.DocumentElement)
            {
                Context.Output.WriteLine("An attribute of the document element cannot be pulled-up.");
                return;
            }

            var clone = attribute.Clone() as XmlAttribute;
            clone.InnerXml = attribute.Value;
            parent.ParentNode.Attributes.Append(clone);
            parent.RemoveAttributeNode(attribute);
        }
    }
}