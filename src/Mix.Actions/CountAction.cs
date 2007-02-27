using System;
using System.IO;
using System.Xml.XPath;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Counts all selected nodes.")]
    public class CountAction : Action
    {
        protected override bool ExecuteCore(IContext context)
        {
            if (!String.IsNullOrEmpty(context.XPath))
            {
                using (TextReader reader = new StringReader(context.Xml))
                {
                    XPathDocument document = new XPathDocument(reader);
                    XPathNavigator navigator = document.CreateNavigator();
                    XPathNodeIterator iterator = navigator.Select(context.XPath);
                    context.Output.WriteLine(iterator.Count);
                }
            }
            else
            {
                context.Output.WriteLine("No selection.");
            }
            return true;
        }
    }
}