using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Unwraps the selected nodes, e.g. '<element>text</element>' becomes 'text' and '<!-- text -->' becomes ' text '.")]
    public class Unwrap : Task
    {
        protected override void ExecuteCore(XmlElement element)
        {
            foreach (XmlNode node in element.ChildNodes)
            {
                var clone = node.CloneNode(true);
                element.ParentNode.InsertBefore(clone, element);
            }
            element.ParentNode.RemoveChild(element);
        }
    }
}