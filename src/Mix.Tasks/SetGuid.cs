using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
	[Description("Sets the value of the selected elements, attributes, text nodes, CDATA sections, comments or processing instructions to a newly generated GUID.")]
	public class SetGuid : Task
	{
		[Option]
		[Description("If set, GUIDs will be generated without dashes, e.g. 7b40db7a7f824fff82289d2500eb37f6 instead of 7b40db7a-7f82-4fff-8228-9d2500eb37f6.")]
		public bool NoDash { get; set; }

		protected override void ExecuteCore(XmlElement element)
		{
			element.InnerXml = NewGuid();
		}

		protected override void ExecuteCore(XmlAttribute attribute)
		{
			attribute.Value = NewGuid();
		}

		protected override void ExecuteCore(XmlText text)
		{
			text.Value = NewGuid();
		}

		protected override void ExecuteCore(XmlCDataSection section)
		{
			section.Value = NewGuid();
		}

		protected override void ExecuteCore(XmlComment comment)
		{
			comment.Value = NewGuid();
		}

		protected override void ExecuteCore(XmlProcessingInstruction instruction)
		{
			instruction.Value = NewGuid();
		}

		// See: http://www.ideablade.com/forum/forum_posts.asp?TID=38&PID=99
		protected virtual string NewGuid()
		{
			var dateBytes = BitConverter.GetBytes(DateTime.Now.Ticks);
			var guidBytes = Guid.NewGuid().ToByteArray();
			// Copy the last six bytes from the date to the last six bytes of the GUID 
			Array.Copy(dateBytes, dateBytes.Length - 7, guidBytes, guidBytes.Length - 7, 6);
			var value = new Guid(guidBytes).ToString();

			if (NoDash)
			{
				return value.Replace("-", "");
			}
			return value;
		}
	}
}