using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class MoveAttributeFixture : TestFixture
    {
        [Test]
        public void MoveToFirstPosition()
        {
            const string pre = @"<root foo="""" bar="""" baz="""" />";
            const string post = @"<root bar="""" foo="""" baz="""" />";
            const string xpath = "root/@bar";
            var task = new MoveAttribute {Position = 1};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void MoveToSamePosition()
        {
            const string pre = @"<root foo="""" bar="""" baz="""" />";
            const string post = pre;
            const string xpath = "root/@bar";
            var task = new MoveAttribute {Position = 2};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void MoveToLastPosition()
        {
            const string pre = @"<root foo="""" bar="""" baz="""" />";
            const string post = @"<root foo="""" baz="""" bar="""" />";
            const string xpath = "root/@bar";
            var task = new MoveAttribute {Position = 3};
            Run(pre, post, xpath, task);
        }
    }
}