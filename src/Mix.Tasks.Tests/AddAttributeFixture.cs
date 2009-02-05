using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class AddAttributeFixture : TestFixture
    {
        [Test]
        public void AddAttributeBySelectingElements()
        {
            const string pre = @"<root />";
            const string post = @"<root post="""" />";
            const string xpath = "root";
            var task = new AddAttribute {Name = "post"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void AddAttributeBySelectingElementsAndUsingAnXPathExpression()
        {
            const string pre = @"<root />";
            const string post = @"<root post="""" />";
            const string xpath = "root";
            var task = new AddAttribute {Name = "post"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void AddAttributesUsingXPathExpression()
        {
            const string pre = @"<root pre=""pre"" />";
            const string post = @"<root pre=""pre"" post=""pre"" />";
            const string xpath = "root";
            var task = new AddAttribute {Name = "post", Value = "xpath:@pre"};
            Run(pre, post, xpath, task);
        }
    }
}