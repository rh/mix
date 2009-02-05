using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Shows an outline of the selected elements.")]
    public class Outline : Task, IReadOnly
    {
        private int depth = 1;

        [Argument]
        public int Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        protected override void OnBeforeExecute(int count)
        {
            Context.Output.WriteLine("{0}: {1}", Context.FileName, count);
        }

        protected override void ExecuteCore(XmlElement element)
        {
            WriteElement(element, "", Depth);
        }

        private void WriteElement(XmlNode element, string indentation, int depth)
        {
            if (depth == 0)
            {
                Context.Output.WriteLine("{0}<{1} />", indentation, element.Name);
            }
            else
            {
                if (element.HasChildNodes)
                {
                    if (element.ChildNodes.Count == 1 && element.FirstChild.NodeType == XmlNodeType.Text)
                    {
                        Context.Output.WriteLine("{0}<{1}></{1}>", indentation, element.Name);
                        return;
                    }
                    else
                    {
                        Context.Output.WriteLine("{0}<{1}>", indentation, element.Name);
                    }
                }
                else
                {
                    Context.Output.WriteLine("{0}<{1} />", indentation, element.Name);
                    return;
                }
            }

            if (depth > 0)
            {
                foreach (XmlNode node in element.ChildNodes)
                {
                    if (node is XmlElement)
                    {
                        WriteElement(node, indentation + "  ", depth - 1);
                    }
                }
                Context.Output.WriteLine("{0}</{1}>", indentation, element.Name);
            }
        }
    }
}