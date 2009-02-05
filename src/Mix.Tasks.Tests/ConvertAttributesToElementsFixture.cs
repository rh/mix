using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class ConvertAttributesToElementsFixture : TestFixture
    {
        [Test]
        public void ElementNotYetPresent()
        {
            const string pre = @"<root foo=""bar"" />";
            const string post = @"<root><foo>bar</foo></root>";
            const string xpath = "//@foo";
            var task = new ConvertAttributesToElements();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ElementAlreadyPresent()
        {
            const string pre = @"<root foo=""bar""><foo>bar</foo></root>";
            const string post = pre;
            const string xpath = "//@foo";
            var task = new ConvertAttributesToElements();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void InsertAsFirstChild()
        {
            const string pre = @"<root foo=""bar""><bar>baz</bar></root>";
            const string post = @"<root><foo>bar</foo><bar>baz</bar></root>";
            const string xpath = "//@foo";
            var task = new ConvertAttributesToElements();
            Run(pre, post, xpath, task);
        }
    }
}