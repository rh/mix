using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class ConvertToAttributeFixture : TestFixture
    {
        [Test]
        public void ElementIsTextNode()
        {
            const string pre = @"<root><foo>bar</foo></root>";
            const string post = @"<root foo=""bar""></root>";
            const string xpath = "//foo";
            var task = new ConvertToAttribute();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ElementIsNotTextNode()
        {
            const string pre = @"<root><foo><bar />bar</foo></root>";
            const string post = pre;
            const string xpath = "//foo";
            var task = new ConvertToAttribute();
            Run(pre, post, xpath, task);
        }
    }
}