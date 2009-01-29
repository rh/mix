using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Makes the value of all selected nodes uppercase.")]
    public class UpperCase : Action
    {
        protected override void ExecuteCore(XmlElement element)
        {
            Recurse(element);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = attribute.Value.ToUpper();
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.Value = text.Value.ToUpper();
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value = section.Value.ToUpper();
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = comment.Value.ToUpper();
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            instruction.Value = instruction.Value.ToUpper();
        }
    }
}