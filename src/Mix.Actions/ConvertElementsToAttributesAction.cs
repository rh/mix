using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    /// <summary>
    /// This action creates a new attribute with the name and value of the
    /// selected element. The new attribute is inserted as the last attribute
    /// of the element's owner element. The element itself is removed.
    /// If the element is not a text node nothing changes.
    /// If the element's owner element already has an attribute with the name of
    /// the element nothing changes.
    /// </summary>
    [Description("Converts elements to attributes.")]
    [Alias("etoa,e-to-a")]
    public class ConvertElementsToAttributesAction : Action
    {
        protected override void ExecuteCore(XmlElement element)
        {
            XmlElement owner = element.ParentNode as XmlElement;
            string name = element.Name;

            if (owner != null && owner.Attributes[name] == null)
            {
                XmlText xmlText = element.FirstChild as XmlText;
                if (xmlText != null)
                {
                    XmlAttribute attribute = element.OwnerDocument.CreateAttribute(name);
                    attribute.Value = xmlText.Value;
                    owner.Attributes.Append(attribute);
                    owner.RemoveChild(element);
                }
            }
        }
    }
}