using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Mix.Core;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;

namespace Mix.Tasks
{
    [Description("Shows all selected nodes.")]
    public class Show : Task, IReadOnly
    {
        [Argument, Description("If set, line numbers are shown before each selected node.")]
        public bool ShowLineNumbers { get; set; }

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
                        if (iterator.Count > 0)
                        {
                            context.Output.WriteLine("{0}: {1}", context.FileName, iterator.Count);
                        }

                        while (iterator.MoveNext())
                        {
                            var current = iterator.Current;
                            var prefix = string.Empty;

                            var info = current as IXmlLineInfo;
                            if (info != null)
                            {
                                prefix = ShowLineNumbers ? string.Format("{0,4}: ", info.LineNumber) : prefix;
                            }

                            var xml = prefix + current.OuterXml.Trim().Replace(Environment.NewLine, Environment.NewLine + new string(' ', prefix.Length));
                            context.Output.WriteLine(xml);
                        }
                    }
                    catch (XPathException e)
                    {
                        var message = string.Format("The specified XPath expression is not valid: {0}", e.Message);
                        throw new TaskExecutionException(message, e);
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