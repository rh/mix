using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("")]
    public sealed class CopyAction : Action
    {
        #region Action Overrides

        protected override void ExecuteCore(XmlElement element)
        {
            XmlElement newelement = element.OwnerDocument.CreateElement(element.Name);
            XmlHelper.CopyAttributes(element.OwnerDocument, element, newelement);
            XmlHelper.CopyChildNodes(element, newelement);
            XmlHelper.ReplaceElement(element, newelement);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            XmlNode parent = attribute.OwnerElement.ParentNode;

            foreach (XmlNode node in parent.ChildNodes)
            {
                XmlElement element = node as XmlElement;
                if (element != null)
                {
                    if (!element.HasAttribute(attribute.Name))
                    {
                        XmlAttribute newattribute = attribute.OwnerDocument.CreateAttribute(attribute.Name);
                        newattribute.Value = attribute.Value;
                        element.Attributes.Append(newattribute);
                    }
                }
            }
        }

        #endregion
    }
}
