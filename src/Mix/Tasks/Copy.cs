using System.Xml;
using Mix.Attributes;

namespace Mix.Tasks
{
    [Description("Inserts copies of the selected nodes after those nodes.")]
    public class Copy : Task
    {
        private void DoCopy(XmlNode node)
        {
            XmlNode clone = node.CloneNode(true);
            node.ParentNode.InsertAfter(clone, node);
        }

        protected override void ExecuteCore(XmlElement element)
        {
            DoCopy(element);
        }

        protected override void ExecuteCore(XmlText text)
        {
            DoCopy(text);
        }

        protected override void ExecuteCore(XmlCDataSection section)
        {
            DoCopy(section);
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            DoCopy(comment);
        }

        protected override void ExecuteCore(XmlProcessingInstruction instruction)
        {
            DoCopy(instruction);
        }
    }
}