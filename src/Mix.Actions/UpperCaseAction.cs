using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Makes all attribute values uppercase.")]
    public sealed class UpperCaseAction : Action
    {
        #region Action Overrides

        protected override void ExecuteCore(XmlElement element)
        {
            foreach (XmlAttribute attribute in element.Attributes)
            {
                attribute.Value = attribute.Value.ToUpper();
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = attribute.Value.ToUpper();
        }

        #endregion
    }
}
