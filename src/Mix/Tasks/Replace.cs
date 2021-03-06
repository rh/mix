using System.Text.RegularExpressions;
using System.Xml;
using Mix.Attributes;

namespace Mix.Tasks
{
    [Description("Replaces text in (the inner XML of) elements and in attributes, text nodes, CDATA sections, comments and processing instructions using regular expressions.")]
    public class Replace : Task
    {
        public Replace()
        {
            Pattern = string.Empty;
            Replacement = string.Empty;
        }

        [RegexOption]
        [Description("A regular expression specifying the value to be replaced.\nIf not set, the whole value will be replaced.")]
        public string Pattern { get; set; }

        [Option(SupportsXPathTemplates = true)]
        [Description("The replacement string, which may contain backreferences (e.g. $1).")]
        public string Replacement { get; set; }

        [Option]
        [Description("If set, replacement will take place in the inner XML of selected elements.")]
        public bool Xml { get; set; }

        private string DoReplace(string value)
        {
            if (!string.IsNullOrEmpty(Pattern))
            {
                return Regex.Replace(value, Pattern, Replacement);
            }

            return Replacement;
        }

        protected override void ExecuteCore(XmlElement element)
        {
            if (Xml)
            {
                element.InnerXml = DoReplace(element.InnerXml);
            }
            else
            {
                if (element.HasChildNodes)
                {
                    if (element.ChildNodes.Count == 1)
                    {
                        if (element.FirstChild is XmlText)
                        {
                            ExecuteCore(element.FirstChild as XmlText);
                        }
                        else if (element.FirstChild is XmlCDataSection)
                        {
                            ExecuteCore(element.FirstChild as XmlCDataSection);
                        }
                    }
                }
                else
                {
                    var text = element.OwnerDocument.CreateTextNode("");
                    element.AppendChild(text);
                    ExecuteCore(text);
                }
            }
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
