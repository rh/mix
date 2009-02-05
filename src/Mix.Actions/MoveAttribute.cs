using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("")]
    public class MoveAttribute : Task
    {
        [Argument, Required]
        [Description("The position of the selected attribute.")]
        public int Position { get; set; }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            var clone = attribute.Clone() as XmlAttribute;
            var parent = attribute.OwnerElement;

            if (Position == 1)
            {
                parent.Attributes.Prepend(clone);
            }
            else if (Position == parent.Attributes.Count)
            {
                parent.Attributes.Append(clone);
            }
            else
            {
                for (var i = 0; i < parent.Attributes.Count; i++)
                {
                    var child = parent.Attributes[i];
                    if (i + 1 == Position)
                    {
                        parent.Attributes.InsertBefore(clone, child);
                        break;
                    }
                }
            }
            parent.Attributes.Remove(attribute);
        }
    }
}