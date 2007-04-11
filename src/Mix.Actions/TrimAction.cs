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
                foreach (XmlNode node in element.ChildNodes)
                {
                    if (node is XmlText)
                    {
                        node.Value = node.Value.Trim();
                    }
                    else if (node is XmlCDataSection)
                    {
                        node.Value = node.Value.Trim();
                    }
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