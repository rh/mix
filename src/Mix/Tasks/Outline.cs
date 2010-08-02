using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Mix.Attributes;

namespace Mix.Tasks
{
    [ReadOnly]
    [Description("Shows all distinct outlines of the selected elements.")]
    public class Outline : Task
    {
        private List<string> outlines = new List<string>();

        public Outline()
        {
            Depth = 1;
        }

        [Option]
        public int Depth { get; set; }

        protected override void OnBeforeExecute(int count)
        {
            outlines = new List<string>();
        }

        protected override void OnAfterExecute()
        {
            Context.Output.WriteLine("{0}: {1}", Context.FileName, outlines.Count);
            outlines.Sort(delegate(string s1, string s2) { return s1.CompareTo(s2); });
            foreach (string outline in outlines)
            {
                Context.Output.WriteLine("{0}", outline);
            }
        }

        protected override void ExecuteCore(XmlDocument document)
        {
            CreateOutlines(document.DocumentElement);
        }

        protected override void ExecuteCore(XmlElement element)
        {
            CreateOutlines(element);
        }

        private void CreateOutlines(XmlNode element)
        {
            var output = new StringBuilder();
            CreateOutline(element, "", Depth, output);
            var outline = output.ToString();

            if (!outlines.Contains(outline))
            {
                outlines.Add(outline);
            }
        }

        private void CreateOutline(XmlNode element, string indentation, int depth, StringBuilder output)
        {
            if (depth == 0)
            {
                output.AppendFormat("{0}<{1} />", indentation, element.Name);
                output.Append(Environment.NewLine);
            }
            else
            {
                if (element.HasChildNodes)
                {
                    if (element.ChildNodes.Count == 1 && element.FirstChild.NodeType == XmlNodeType.Text)
                    {
                        output.AppendFormat("{0}<{1}></{1}>", indentation, element.Name);
                        output.Append(Environment.NewLine);
                        return;
                    }
                    output.AppendFormat("{0}<{1}>", indentation, element.Name);
                    output.Append(Environment.NewLine);
                }
                else
                {
                    output.AppendFormat("{0}<{1} />", indentation, element.Name);
                    output.Append(Environment.NewLine);
                    return;
                }
            }

            if (depth > 0)
            {
                foreach (XmlNode node in element.ChildNodes)
                {
                    if (node is XmlElement)
                    {
                        CreateOutline(node, indentation + "  ", depth - 1, output);
                    }
                }
                output.AppendFormat("{0}</{1}>", indentation, element.Name);
                output.Append(Environment.NewLine);
            }
        }
    }
}