using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class AddElementActionFixture : TestFixture
    {
        [Test]
        public void AddElementWithoutValue()
        {
            const string pre = @"<root />";
            const string post = @"<root><name></name></root>";
            const string xpath = "root";
            AddElement action = new AddElement();
            action.Name = "name";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AddElementWithValue()
        {
            const string pre = @"<root />";
            const string post = @"<root><name>value</name></root>";
            const string xpath = "root";
            AddElement action = new AddElement();
            action.Name = "name";
            action.Value = "value";
            Run(pre, post, xpath, action);
        }
    }
}