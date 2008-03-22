using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class AddAttributeActionFixture : TestFixture
    {
        [Test]
        public void AddAttributeBySelectingElements()
        {
            const string pre = @"<root />";
            const string post = @"<root post="""" />";
            const string xpath = "root";
            AddAttributeAction action = new AddAttributeAction();
            action.Name = "post";
            Run(pre, post, xpath, action);
        }
        [Test]
        public void AddAttributeBySelectingElementsAndUsingAnXPathExpression()
        {
            const string pre = @"<root />";
            const string post = @"<root post="""" />";
            const string xpath = "root";
            AddAttributeAction action = new AddAttributeAction();
            action.Name = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AddAttributesUsingXPathExpression()
        {
            const string pre = @"<root pre=""pre"" />";
            const string post = @"<root pre=""pre"" post=""pre"" />";
            const string xpath = "root";
            AddAttributeAction action = new AddAttributeAction();
            action.Name = "post";
            action.Value = "xpath:@pre";
            Run(pre, post, xpath, action);
        }
    }
}