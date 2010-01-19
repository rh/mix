using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Joins two or more nodes.")]
    public class Join : Task
    {
        public Join()
        {
            Text = true;
        }

        [Option, Required]
        [Description("An XPath expression, applied to the selected node, which determines which node(s) will be joined with the selected node.")]
        public string With { get; set; }

        [Option]
        [Description("")]
        public string Separator { get; set; }

        [Option]
        [Description("If set, the textual representation of elements will be joined. This is the default.")]
        public bool Text { get; set; }

        [Option]
        [Description("If set, the inner XML of elements, that is the XML excluding the selected element,  will be joined.")]
        public bool InnerXml { get; set; }

        [Option]
        [Description("If set, the outer XML of elements, that is the XML including the selected element, will be joined.")]
        public bool OuterXml { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            var nodes = element.SelectNodes(With);

            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    string xml;

                    if (InnerXml)
                    {
                        xml = node.InnerXml;
                    }
                    else if (OuterXml)
                    {
                        xml = node.OuterXml;
                    }
                    else
                    {
                        xml = node.InnerText;
                    }

                    if (element.InnerXml.Length == 0)
                    {
                        element.InnerXml = element.InnerXml + xml;
                    }
                    else
                    {
                        element.InnerXml = element.InnerXml + Separator + xml;
                    }

                    node.ParentNode.RemoveChild(node);
                }
            }
        }
    }
}