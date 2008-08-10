using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class ConvertElementsToAttributesActionFixture : TestFixture
    {
        [Test]
        public void ElementIsTextNode()
        {
            const string pre = @"<root><foo>bar</foo></root>";
            const string post = @"<root foo=""bar""></root>";
            const string xpath = "//foo";
            ConvertElementsToAttributes action =
                new ConvertElementsToAttributes();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ElementIsNotTextNode()
        {
            const string pre = @"<root><foo><bar />bar</foo></root>";
            const string post = pre;
            const string xpath = "//foo";
            ConvertElementsToAttributes action =
                new ConvertElementsToAttributes();
            Run(pre, post, xpath, action);
        }
    }
}