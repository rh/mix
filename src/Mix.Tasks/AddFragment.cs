using System.Xml;
using System.Xml.XPath;
using Mix.Core;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;

namespace Mix.Tasks
{
    [Description("Adds an XML fragment to the selected elements.")]
    public class AddFragment : Task
    {
        [XmlOption, Required]
        [Description("The raw XML of the new element.")]
        public string Fragment { get; set; }

        [Option]
        [Description("An XPath expression, applied to the selected element, which determines before which child the new element is added.")]
        public string Before { get; set; }

        [Option]
        [Description("An XPath expression, applied to the selected element, which determines after which child the new element is added.")]
        public string After { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            var child = element.OwnerDocument.CreateDocumentFragment();
            child.InnerXml = Fragment;

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
            }

            element.AppendChild(child);
        }
    }
}