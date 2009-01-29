using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;
using Environment = System.Environment;

namespace Mix.Actions
{
    [Description("Replaces text in the text nodes of selected elements or in the values of selected attributes.\nUse \\n for new-line and \\t for tab.")]
    public class Replace : Action
    {
        public Replace()
        {
            OldValue = string.Empty;
            NewValue = string.Empty;
        }

        [Argument, Required, Description("The value to be replaced.")]
        public string OldValue { get; set; }

        [Argument, Description("The value to replace the old value.")]
        public string NewValue { get; set; }

        protected override void OnBeforeExecute(int count)
        {
            OldValue = OldValue.Replace("\\n", Environment.NewLine);
            OldValue = OldValue.Replace("\\t", "\t");

            NewValue = NewValue.Replace("\\n", Environment.NewLine);
            NewValue = NewValue.Replace("\\t", "\t");
        }

        protected override void ExecuteCore(XmlElement element)
        {
            element.InnerXml = element.InnerXml.Replace(OldValue, NewValue);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = attribute.Value.Replace(OldValue, NewValue);
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.Value = text.Value.Replace(OldValue, NewValue);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value = section.Value.Replace(OldValue, NewValue);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = comment.Value.Replace(OldValue, NewValue);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            instruction.Value = instruction.Value.Replace(OldValue, NewValue);
        }
    }
}