using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
	[Description("Unwraps the selected nodes, e.g. '<element>text</element>' becomes 'text' and '<!-- text -->' becomes ' text '.")]
	public class Unwrap : Task
	{
		protected override void ExecuteCore(XmlElement element)
		{
			foreach (XmlNode node in element.ChildNodes)
			{
				var clone = node.CloneNode(true);
				element.ParentNode.InsertBefore(clone, element);
			}
			element.ParentNode.RemoveChild(element);
		}

		protected override void ExecuteCore(XmlCDataSection section)
		{
			var clone = section.OwnerDocument.CreateTextNode(section.Value);
			section.ParentNode.InsertBefore(clone, section);
			section.ParentNode.RemoveChild(section);
		}

		protected override void ExecuteCore(XmlComment comment)
		{
			var clone = comment.OwnerDocument.CreateTextNode(comment.Value);
			comment.ParentNode.InsertBefore(clone, comment);
			comment.ParentNode.RemoveChild(comment);
		}

		protected override void ExecuteCore(XmlProcessingInstruction instruction)
		{
			var clone = instruction.OwnerDocument.CreateTextNode(instruction.Value);
			instruction.ParentNode.InsertBefore(clone, instruction);
			instruction.ParentNode.RemoveChild(instruction);
		}
	}
}