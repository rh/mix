using NUnit.Framework;

namespace Mix.Tasks.Tests
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
            var task = new PullUp();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void PullUpAttribute()
        {
            const string pre = @"<a><b><c foo=""bar""></c></b></a>";
            const string post = @"<a><b foo=""bar""><c></c></b></a>";
            const string xpath = "a/b/c/@foo";
            var task = new PullUp();
            Run(pre, post, xpath, task);
        }
    }
}