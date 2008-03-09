using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Shows all selected nodes.")]
    public class ShowAction : Action, IReadOnly
    {
        protected override void ExecuteCore(XmlNode node)
        {
            string xml = node.OuterXml.Trim();
            xml = xml.Replace(Environment.NewLine, String.Format("{0}  ", Environment.NewLine));
            Context.Output.WriteLine("  {0}", xml);
        }
    }
}