using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Copies the values of the selected attributes to new or existing attributes.")]
    public class CopyAttribute : Task
    {
        private string name = string.Empty;

        [Argument, Required]
        [Description("The name of the new or existing attribute.")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            var owner = attribute.OwnerElement;

            if (owner.HasAttribute(Name))
            {
                owner.GetAttributeNode(Name).Value = attribute.Value;
            }
            else
            {
                XmlHelper.AddAttribute(attribute.OwnerDocument, owner, Name, attribute.Value);
            }
        }
    }
}