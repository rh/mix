using System;
using System.Xml;
using System.Xml.XPath;
using Mix.Core;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;

namespace Mix.Tasks
{
    public abstract class AddNode : Task
    {
        [Option]
        [Description("An XPath expression, applied to the selected element, which determines before which child the new node is added.")]
        public string Before { get; set; }

        [Option]
        [Description("An XPath expression, applied to the selected element, which determines after which child the new node is added.")]
        public string After { get; set; }

        protected abstract XmlNode CreateNode(XmlElement element);

        protected override void ExecuteCore(XmlElement element)
        {
            var child = CreateNode(element);

            if (!string.IsNullOrEmpty(After))
            {
                try
                {
                    var node = element.SelectSingleNode(After);
                    if (node != null)
                    {
                        element.InsertAfter(child, node);
                        return;
                    }
                }
                catch (XPathException e)
                {
                    var message = string.Format("'{0}' is not a valid XPath expression.", After);
                    throw new TaskExecutionException(message, e);
                }
                catch (ArgumentException)
                {
                    // The XPath expression is not valid IN THIS CONTEXT, e.g. an attribute was selected
                }
            }

            if (!string.IsNullOrEmpty(Before))
            {
                try
                {
                    var node = element.SelectSingleNode(Before);
                    if (node != null)
                    {
                        element.InsertBefore(child, node);
                        return;
                    }
                }
                catch (XPathException e)
                {
                    var message = string.Format("'{0}' is not a valid XPath expression.", Before);
                    throw new TaskExecutionException(message, e);
                }
                catch (ArgumentException)
                {
                    // The XPath expression is not valid IN THIS CONTEXT, e.g. an attribute was selected
                }
            }

            element.AppendChild(child);
        }
    }
}