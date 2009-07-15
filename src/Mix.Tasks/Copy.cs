using System.Text.RegularExpressions;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Inserts copies of the selected elements after those elements.")]
    public class Copy : Task
    {
        protected override void ExecuteCore(XmlElement element)
        {
            XmlNode clone = element.CloneNode(true);
            element.ParentNode.InsertAfter(clone, element);
        }
    }
}