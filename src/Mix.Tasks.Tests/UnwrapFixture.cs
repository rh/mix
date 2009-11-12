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
	}
}