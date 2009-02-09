using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Sets the inner XML of the selected elements or comments.")]
    public class InnerXml : Task
    {
        private string xml = string.Empty;

        [Argument, XmlArgument, Required]
        [Description("The literal XML of the selected elements.")]
        public string Xml
        {
            get { return xml; }
            set { xml = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            element.InnerXml = Xml;
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            // InnerText is used because InnerXml will throw;
            // an XmlComment cannot have children.
            comment.InnerText = Xml;
        }
    }
}