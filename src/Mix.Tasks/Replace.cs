using System.Text.RegularExpressions;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Replaces text in the inner XML of elements and in attributes, text nodes, CDATA sections, comments and processing instructions using regular expressions.")]
    public class Replace : Task
    {
        public Replace()
        {
            Pattern = string.Empty;
            Replacement = string.Empty;
        }

        [Argument, Required, Description("A regular expression specifying the value to be replaced.")]
        public string Pattern { get; set; }

        [Argument, Description("The replacement string, which may contain backreferences (e.g. $1).")]
        public string Replacement { get; set; }

        [Argument, Description("If set, case-insensitive matching will be attempted. The default is case-sensitive matching.")]
        public bool IgnoreCase { get; set; }

        [Argument, Description("If set, . matches every character, instead of every character except \\n.")]
        public bool Singleline { get; set; }

        [Argument, Description("If set, ^ and $ match the beginning and end of any line, instead of the whole string.")]
        public bool Multiline { get; set; }

        private string DoReplace(string value)
        {
            var options = RegexOptions.None;
            if (IgnoreCase)
            {
                options |= RegexOptions.IgnoreCase;
            }
            if (Singleline)
            {
                options |= RegexOptions.Singleline;
            }
            if (Multiline)
            {
                options |= RegexOptions.Multiline;
            }
            return Regex.Replace(value, Pattern, Replacement, options);
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