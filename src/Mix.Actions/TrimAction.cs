using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Trims the text nodes of the selected elements, " +
                 "or the value of the selected attributes.")]
    public class TrimAction : Action
    {
        #region Action Overrides

        protected override void ExecuteCore(XmlElement element)
        {
            if (element.HasChildNodes)
            {
                if (element.FirstChild is XmlText)
                {
                    element.FirstChild.Value = element.FirstChild.Value.Trim();
                }
                else if (element.FirstChild is XmlCDataSection)
                {
                    element.FirstChild.Value = element.FirstChild.Value.Trim();
                }
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = attribute.Value.Trim();
        }

        #endregion
    }
}