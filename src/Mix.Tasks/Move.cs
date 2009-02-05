using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    public class Move : Task
    {
        [Argument, Required]
        [Description("An XPath expression, relative to the selected node, which determines to which node the selected node should be moved.")]
        public string To { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            // TODO: validate XPath expression
            var newParent = element.SelectSingleNode(To) as XmlElement;
            if (newParent != null)
            {
                var clone = element.CloneNode(true);
                newParent.AppendChild(clone);
                element.ParentNode.RemoveChild(element);
            }
            else
            {
                Context.Output.WriteLine("");
            }
        }
    }
}