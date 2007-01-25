using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Makes all attribute values lowercase.")]
    public sealed class LowerCaseAction : Action
    {
        #region Action Overrides

        protected override void ExecuteCore(XmlElement element)
        {
            if (ActionType == ActionType.Property)
            {
                // TODO: implement ExecuteCore(XmlElement) for the element itself
            }
            else
            {
                if (element.HasChildNodes)
                {
                    if (element.FirstChild is XmlText)
                    {
                        element.InnerText = element.InnerText.ToLower();
                    }
                    else if (element.FirstChild is XmlCDataSection)
                    {
                        XmlCDataSection section = (XmlCDataSection) element.FirstChild;
                        section.Value = section.Value.ToLower();
                    }
                }
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            if (ActionType == ActionType.Property)
            {
                XmlDocument document = attribute.OwnerDocument;
                XmlElement element = attribute.OwnerElement;
                string name = attribute.Name.ToLower();
                string value = attribute.Value;

                if (element.HasAttribute(name))
                {
                    return;
                }

                if (element.Attributes.Count == 1)
                {
                    element.RemoveAttribute(attribute.Name);
                    XmlHelper.AddAttribute(document, element, name, value);
                }
                else
                {
                    XmlAttribute before = null;
                    foreach (XmlAttribute xmlAttribute in element.Attributes)
                    {
                        if (xmlAttribute == attribute)
                        {
                            element.RemoveAttribute(attribute.Name);
                            XmlAttribute newAttribute = document.CreateAttribute(name);
                            newAttribute.Value = value;

                            if (before == null)
                            {
                                element.Attributes.Prepend(newAttribute);
                            }
                            else
                            {
                                element.Attributes.InsertAfter(newAttribute, before);
                            }
                            break;
                        }
                        before = xmlAttribute;
                    }
                }
            }
            else
            {
                attribute.Value = attribute.Value.ToLower();
            }
        }

        #endregion
    }
}
