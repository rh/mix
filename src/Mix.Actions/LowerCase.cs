using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Makes the value of all selected nodes lowercase.")]
    public class LowerCase : Mix.Core.Action
    {
        protected override void ExecuteCore(XmlElement element)
        {
            Recurse(element);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = attribute.Value.ToLower();
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.Value = text.Value.ToLower();
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value = section.Value.ToLower();
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = comment.Value.ToLower();
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            instruction.Value = instruction.Value.ToLower();
        }
    }
}