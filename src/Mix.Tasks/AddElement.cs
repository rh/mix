using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Adds a new element.")]
    public class AddElement : Task
    {
        [Option(SupportsXPathTemplates = true), Required]
        [Description("The name of the new element.")]
        public string Name { get; set; }

        [Option(SupportsXPathTemplates = true)]
        [Description("The value of the new element.")]
        public string Value { get; set; }

        [Option]
        [Description("An XPath expression, applied to the selected element, which determines before which child the new element is added.")]
        public string Before { get; set; }

        [Option]
        [Description("An XPath expression, applied to the selected element, which determines after which child the new element is added.")]
        public string After { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            var child = element.OwnerDocument.CreateElement(Name);
            child.InnerText = Value;

            if (!string.IsNullOrEmpty(After))
            {
                var node = element.SelectSingleNode(After);
                if (node != null)
                {
                    element.InsertAfter(child, node);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(Before))
            {
                var node = element.SelectSingleNode(Before);
                if (node != null)
                {
                    element.InsertBefore(child, node);
                    return;
                }
            }

            element.AppendChild(child);
        }
    }
}