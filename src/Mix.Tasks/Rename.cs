using System.Text.RegularExpressions;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Renames all selected elements, attributes or processing instructions.")]
    [Reversed]
    public class Rename : Task
    {
        public Rename()
        {
            Pattern = string.Empty;
            Name = string.Empty;
        }

        [RegexOption]
        [Description("A regular expression specifying (part of) the name to be changed.\nLeave blank to specify the whole name.")]
        public string Pattern { get; set; }

        [Option(SupportsXPathTemplates = true), Required]
        [Description("The new name, or a replacement string, which may contain backreferences (e.g. $1).")]
        public string Name { get; set; }

        [Option]
        [Description("If set, case-insensitive matching will be attempted. The default is case-sensitive matching.")]
        public bool IgnoreCase { get; set; }

        private string DoRename(string name)
        {
            var options = RegexOptions.None;
            if (IgnoreCase)
            {
                options |= RegexOptions.IgnoreCase;
            }
            if (string.IsNullOrEmpty(Pattern))
            {
                Pattern = name;
            }
            return Regex.Replace(name, Pattern, Name, options);
        }

        protected override void ExecuteCore(XmlElement element)
        {
            // TODO: what if element is DocumentElement?
            if (element.OwnerDocument.DocumentElement == element)
            {
                Context.Error.WriteLine("{0}: renaming the root element is currently not supported.", Context.FileName);
                return;
            }

            var clone = element.OwnerDocument.CreateElement(DoRename(element.Name));
            foreach (XmlAttribute attribute in element.Attributes)
            {
                clone.Attributes.Append(attribute.Clone() as XmlAttribute);
            }
            foreach (XmlNode child in element.ChildNodes)
            {
                clone.AppendChild(child.Clone());
            }
            element.ParentNode.ReplaceChild(clone, element);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            var name = DoRename(attribute.Name);
            var owner = attribute.OwnerElement;

            if (owner.HasAttribute(name))
            {
                Context.Error.WriteLine("{0}: cannot rename attribute '{1}' to '{2}' because an attribute with that name already exists.", Context.FileName, attribute.Name, name);
                return;
            }

            var clone = attribute.OwnerDocument.CreateAttribute(name);
            clone.Value = attribute.Value;
            owner.Attributes.InsertBefore(clone, attribute);
            owner.Attributes.Remove(attribute);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            var parent = instruction.ParentNode;
            var clone = instruction.OwnerDocument.CreateProcessingInstruction(DoRename(instruction.Name), instruction.Value);
            parent.ReplaceChild(clone, instruction);
        }
    }
}