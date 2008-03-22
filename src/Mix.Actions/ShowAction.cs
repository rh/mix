using System;
using System.IO;
using System.Xml.XPath;
using Mix.Core;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;

namespace Mix.Actions
{
    [Description("Shows all selected nodes.")]
    public class ShowAction : Action, IReadOnly
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
                        while (iterator.MoveNext())
                        {
                            context.Output.WriteLine(iterator.Current.OuterXml.Trim());
                        }
                    }
                    catch (XPathException e)
                    {
                        string message = String.Format("'{0}' is not a valid XPath expression: {1}{2}", context.XPath, Environment.NewLine, e.Message);
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