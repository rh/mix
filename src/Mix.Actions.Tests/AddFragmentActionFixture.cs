using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class AddFragmentActionFixture : TestFixture
    {
        [Test]
        public void Element()
        {
            const string fragment = "<child>Some text</child>";
            const string pre = "<root />";
            const string post = "<root>" + fragment + "</root>";
            const string xpath = "root";
            AddFragment action = new AddFragment();
            action.Fragment = fragment;
            Run(pre, post, xpath, action);
        }
    }
}