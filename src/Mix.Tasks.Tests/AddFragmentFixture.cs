using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class AddFragmentFixture : TestFixture
    {
        [Test]
        public void Element()
        {
            const string fragment = "<child>Some text</child>";
            const string pre = "<root />";
            const string post = "<root>" + fragment + "</root>";
            const string xpath = "root";
            var task = new AddFragment {Fragment = fragment};
            Run(pre, post, xpath, task);
        }
    }
}