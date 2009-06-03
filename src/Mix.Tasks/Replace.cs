using System;
using System.Xml;
using System.Text.RegularExpressions;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Replaces text in the text nodes of selected elements or in the values of selected attributes. Regular expressions are allowed.")]
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
				
        private string DoReplace(string value)
        {
            return Regex.Replace(value, Old, New);
        }

        protected override void ExecuteCore(XmlElement element)
        {
            element.InnerXml = DoReplace(element.InnerXml);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = DoReplace(attribute.Value);
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.Value = DoReplace(text.Value);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value = DoReplace(section.Value);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = DoReplace(comment.Value);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            instruction.Value = DoReplace(instruction.Value);
        }
    }
}
