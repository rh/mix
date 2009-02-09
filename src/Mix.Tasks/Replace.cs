using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Replaces text in the text nodes of selected elements or in the values of selected attributes.\nUse \\n for new-line and \\t for tab.")]
    public class Replace : Task
    {
        public Replace()
        {
            Old = string.Empty;
            New = string.Empty;
        }

        [Argument, Required, Description("The value to be replaced.")]
        public string Old { get; set; }

        [Argument, Description("The value to replace the old value.")]
        public string New { get; set; }

        protected override void OnBeforeExecute(int count)
        {
            Old = Old.Replace("\\n", Environment.NewLine);
            Old = Old.Replace("\\t", "\t");

            New = New.Replace("\\n", Environment.NewLine);
            New = New.Replace("\\t", "\t");
        }

        protected override void ExecuteCore(XmlElement element)
        {
            element.InnerXml = element.InnerXml.Replace(Old, New);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = attribute.Value.Replace(Old, New);
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.Value = text.Value.Replace(Old, New);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value = section.Value.Replace(Old, New);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = comment.Value.Replace(Old, New);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            instruction.Value = instruction.Value.Replace(Old, New);
        }
    }
}