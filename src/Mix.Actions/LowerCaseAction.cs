using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Makes the value of all selected nodes lowercase.")]
    public class LowerCaseAction : Action
    {
        #region Action Overrides

        protected override void ExecuteCore(XmlElement element)
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

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = attribute.Value.ToLower();
        }

        #endregion
    }
}