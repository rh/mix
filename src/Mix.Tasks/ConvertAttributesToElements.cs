using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    /// <summary>
    /// This action creates a new element with the name and value of the
    /// attribute. The new element is inserted as the first child element of the
    /// attribute's owner element. The attribute itself is removed.
    /// If the attribute's owner element already has a child element with the
    /// name of the attribute nothing changes.
    /// </summary>
    [Description("Converts attributes to elements.")]
    [Alias("atoe")]
    public class ConvertAttributesToElements : Task
    {
        protected override void ExecuteCore(XmlAttribute attribute)
        {
            var owner = attribute.OwnerElement;
            var name = attribute.Name;

            // Only create a new element if no such element exists yet
            if (owner.SelectSingleNode(name) == null)
            {
                var newElement = attribute.OwnerDocument.CreateElement(name);
                newElement.InnerText = attribute.Value;
                owner.InsertBefore(newElement, owner.FirstChild);
                owner.RemoveAttribute(name);
            }
        }
    }
}