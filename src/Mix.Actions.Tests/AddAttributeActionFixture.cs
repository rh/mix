using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class AddAttributeActionFixture : TestFixture
    {
        [Test]
        public void AddElements()
        {
            const string pre = @"<root />";
            const string post = @"<root post="""" />";
            const string xpath = "root";
            AddAttributeAction action = new AddAttributeAction();
            action.Name = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AddAttributes()
        {
            const string pre = @"<root pre="""" />";
            const string post = @"<root pre="""" post=""value"" />";
            const string xpath = "root/@pre";
            AddAttributeAction action = new AddAttributeAction();
            action.Name = "post";
            action.Value = "value";
            Run(pre, post, xpath, action);
        }
    }
}