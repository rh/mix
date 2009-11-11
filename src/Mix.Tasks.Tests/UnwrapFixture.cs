using NUnit.Framework;

namespace Mix.Tasks.Tests
{
	[TestFixture]
	public class UnwrapFixture : TestFixture
	{
		[Test]
		public void Element()
		{
			const string pre = "<root><parent>1</parent></root>";
			const string post = "<root>1</root>";
			const string xpath = "//parent";
			var task = new Unwrap();
			Run(pre, post, xpath, task);
		}

		[Test]
		public void ElementHasAttributes()
		{
			const string pre = "<root><parent a=\"\" b=\"\">1</parent></root>";
			const string post = "<root>1</root>";
			const string xpath = "//parent";
			var task = new Unwrap();
			Run(pre, post, xpath, task);
		}

		[Test]
		public void Elements()
		{
			const string pre = "<root><parent>1</parent><parent>2</parent></root>";
			const string post = "<root>12</root>";
			const string xpath = "//parent";
			var task = new Unwrap();
			Run(pre, post, xpath, task);
		}

		[Test]
		public void ElementWithChildElements()
		{
			const string pre = "<root><parent><child /><child /></parent></root>";
			const string post = "<root><child /><child /></root>";
			const string xpath = "//parent";
			var task = new Unwrap();
			Run(pre, post, xpath, task);
		}

		[Test]
		public void EmptyElements()
		{
			const string pre = "<root><parent></parent><parent></parent></root>";
			const string post = "<root></root>";
			const string xpath = "//parent";
			var task = new Unwrap();
			Run(pre, post, xpath, task);
		}

		[Test]
		public void CDataSection()
		{
			const string pre = "<root><![CDATA[data]]></root>";
			const string post = "<root>data</root>";
			const string xpath = "//text()";
			var task = new Unwrap();
			Run(pre, post, xpath, task);
		}

		[Test, Ignore("Evaluating '<![CDATA[1]]><![CDATA[2]]>' with '//text()' will only select the first node.")]
		public void CDataSections()
		{
			const string pre = "<root><![CDATA[1]]><![CDATA[2]]></root>";
			const string post = "<root>12</root>";
			const string xpath = "//text()";
			var task = new Unwrap();
			Run(pre, post, xpath, task);
		}

		[Test]
		public void Comments()
		{
			const string pre = "<root><!--1--><!--2--></root>";
			const string post = "<root>12</root>";
			const string xpath = "//comment()";
			var task = new Unwrap();
			Run(pre, post, xpath, task);
		}

		[Test]
		public void EmptyComments()
		{
			const string pre = "<root><!----><!----></root>";
			const string post = "<root></root>";
			const string xpath = "//comment()";
			var task = new Unwrap();
			Run(pre, post, xpath, task);
		}

		[Test]
		public void ProcessingInstructions()
		{
			const string pre = "<root><?foo 1?><?foo 2?></root>";
			const string post = "<root>12</root>";
			const string xpath = "//processing-instruction()";
			var task = new Unwrap();
			Run(pre, post, xpath, task);
		}

		[Test]
		public void EmptyProcessingInstructions()
		{
			const string pre = "<root><?foo ?><?foo ?></root>";
			const string post = "<root></root>";
			const string xpath = "//processing-instruction()";
			var task = new Unwrap();
			Run(pre, post, xpath, task);
		}
	}
}