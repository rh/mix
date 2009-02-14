using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Adds a new CDATA section.")]
    public class AddCdataSection : Task
    {
        private string @value = string.Empty;

        [Argument]
        [Description("The value of the CDATA section.")]
        public string Value
        {
            get { return @value; }
            set { this.@value = value; }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            var section = element.OwnerDocument.CreateCDataSection(Value);
            element.AppendChild(section);
        }
    }
}