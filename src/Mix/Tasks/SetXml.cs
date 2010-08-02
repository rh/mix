using System.Xml;
using Mix.Attributes;

namespace Mix.Tasks
{
    [Description("Sets the inner XML of the selected elements or comments.")]
    public class SetXml : Task
    {
        [XmlOption, Required]
        [Description("The inner XML of the selected elements.")]
        public string Xml { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            element.InnerXml = Xml;
        }

        protected override void ExecuteCore(XmlComment comment)
        {
            // InnerText is used because InnerXml will throw: an XmlComment cannot have children.
            comment.InnerText = Xml;
        }
    }
}