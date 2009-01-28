using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class PullUpFixture : TestFixture
    {
        [Test]
        public void Test()
        {
            const string pre = @"<a><b><c></c></b></a>";
            const string post = @"<a><c></c><b></b></a>";
            const string xpath = "a/b/c";
            var action = new PullUp();
            Run(pre, post, xpath, action);
        }
    }
}