using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class PullUpFixture : TestFixture
    {
        [Test]
        public void PullUpElement()
        {
            const string pre = @"<a><b><c></c></b></a>";
            const string post = @"<a><c></c><b></b></a>";
            const string xpath = "a/b/c";
            var action = new PullUp();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void PullUpAttribute()
        {
            const string pre = @"<a><b><c foo=""bar""></c></b></a>";
            const string post = @"<a><b><foo>bar</foo><c></c></b></a>";
            const string xpath = "a/b/c/@foo";
            var action = new PullUp();
            Run(pre, post, xpath, action);
        }
    }
}