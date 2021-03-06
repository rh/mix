using System.Text.RegularExpressions;
using System.Xml;
using Mix.Attributes;

namespace Mix.Tasks
{
    public abstract class TextTransformer : Task
    {
        [RegexOption, Description("A regular expression specifying the parts of the value to be changed.\nIf not set, the whole value will be changed.")]
        public string Pattern { get; set; }

        protected string Transform(string value)
        {
            if (!string.IsNullOrEmpty(Pattern))
            {
                return Regex.Replace(value, Pattern, Transform);
            }

            return TransformCore(value);
        }

        protected string Transform(Match match)
        {
            return TransformCore(match.Value);
        }

        protected virtual string TransformCore(string value)
        {
            return value;
        }

        protected override void ExecuteCore(XmlElement element)
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
                var newElement = element.OwnerDocument.CreateTextNode(string.Empty);
                element.AppendChild(newElement);
                ExecuteCore(newElement);
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            attribute.Value = Transform(attribute.Value);
        }

        protected override void ExecuteCore(XmlText text)
        {
            text.Value = Transform(text.Value);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            section.Value = Transform(section.Value);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            comment.Value = Transform(comment.Value);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            instruction.Value = Transform(instruction.Value);
        }
    }
}
