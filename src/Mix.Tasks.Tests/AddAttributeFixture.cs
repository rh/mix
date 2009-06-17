using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class AddAttributeFixture : TestFixture
    {
        [Test]
        public void SetNameOnly()
        {
            const string pre = @"<root />";
            const string post = @"<root post="""" />";
            const string xpath = "root";
            var task = new AddAttribute {Name = "post"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void SetNameAndValue()
        {
            const string pre = @"<root />";
            const string post = @"<root post=""foo"" />";
            const string xpath = "root";
            var task = new AddAttribute {Name = "post", Value = "foo"};
            Run(pre, post, xpath, task);
        }
    }
}