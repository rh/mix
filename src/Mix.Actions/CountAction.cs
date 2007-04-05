using System;
using System.IO;
using System.Xml.XPath;
using Mix.Core;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;

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
                    try
                    {
                        XPathNodeIterator iterator = navigator.Select(context.XPath);
                        context.Output.WriteLine("{0}: {1}", context.FileName, iterator.Count);
                    }
                    catch (XPathException e)
                    {
                        string message =
                            String.Format("XPath expression '{0}' is not valid.",
                                          context.XPath);
                        throw new ActionExecutionException(message, e);
                    }
                }
            }
            else
            {
                context.Output.WriteLine("{0}: no selection.", context.FileName);
            }
            return true;
        }
    }
}