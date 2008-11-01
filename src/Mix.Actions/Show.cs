using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Mix.Core;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;

namespace Mix.Actions
{
    [Description("Shows all selected nodes.")]
    public class Show : Action, IReadOnly
    {
        private bool showLineNumbers;

        public bool ShowLineNumbers
        {
            get { return showLineNumbers; }
            set { showLineNumbers = value; }
        }

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
                            XPathNavigator current = iterator.Current;
                            IXmlLineInfo info = current as IXmlLineInfo;
                            if (info != null)
                            {
                                string prefix = showLineNumbers ? String.Format("{0,4}: ", info.LineNumber) : String.Empty;
                                string xml = prefix + current.OuterXml.Trim().Replace(Environment.NewLine, Environment.NewLine + new string(' ', prefix.Length));
                                context.Output.WriteLine(xml);
                            }
                            else
                            {
                                context.Output.WriteLine(current.OuterXml.Trim());
                            }
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
