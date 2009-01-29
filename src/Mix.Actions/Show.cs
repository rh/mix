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
            if (!string.IsNullOrEmpty(context.XPath))
            {
                using (TextReader reader = new StringReader(context.Xml))
                {
                    var document = new XPathDocument(reader);
                    var navigator = document.CreateNavigator();
                    try
                    {
                        var iterator = navigator.Select(context.XPath);
                        context.Output.WriteLine("{0}: {1}", context.FileName, iterator.Count);
                        while (iterator.MoveNext())
                        {
                            var current = iterator.Current;
                            var info = current as IXmlLineInfo;
                            if (info != null)
                            {
                                var prefix = showLineNumbers ? string.Format("{0,4}: ", info.LineNumber) : string.Empty;
                                var xml = prefix + current.OuterXml.Trim().Replace(System.Environment.NewLine, System.Environment.NewLine + new string(' ', prefix.Length));
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
                        var message = string.Format("'{0}' is not a valid XPath expression: {1}{2}", context.XPath, System.Environment.NewLine, e.Message);
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