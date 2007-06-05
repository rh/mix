using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Clears the text nodes of elements, " +
                 "or the value of attributes or comments.")]
    public class ClearAction : Action
    {
        protected override void ExecuteCore(XmlElement element)
        {
            if (element.HasChildNodes)
            {
                foreach (XmlNode node in element.ChildNodes)
                {
                    if (node is XmlText)
                    {
                        node.Value = String.Empty;
                    }
                    else if (node is XmlCDataSection)
                    {
                        node.Value = String.Empty;
                    }
                }
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = String.Empty;
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = String.Empty;
        }
    }
}