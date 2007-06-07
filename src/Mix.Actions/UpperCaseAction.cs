using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Makes the value of all selected nodes uppercase.")]
    public class UpperCaseAction : Action
    {
        protected override void ExecuteCore(XmlElement element)
        {
            if (element.HasChildNodes)
            {
                foreach (XmlNode node in element.ChildNodes)
                {
                    if (node is XmlText)
                    {
                        ExecuteCore(node as XmlText);
                    }
                    else if (node is XmlCDataSection)
                    {
                        node.Value = node.Value.ToUpper();
                    }
                }
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = attribute.Value.ToUpper();
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.Value = text.Value.ToUpper();
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = comment.Value.ToUpper();
        }
    }
}