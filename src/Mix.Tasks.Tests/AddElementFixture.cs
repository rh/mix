using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class AddElementFixture : TestFixture
    {
        [Test]
        public void AddElementWithoutValue()
        {
            const string pre = @"<root />";
            const string post = @"<root><name></name></root>";
            const string xpath = "root";
            var action = new AddElement {Name = "name"};
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AddElementWithValue()
        {
            const string pre = @"<root />";
            const string post = @"<root><name>value</name></root>";
            const string xpath = "root";
            var action = new AddElement {Name = "name", Value = "value"};
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AddElementBefore()
        {
            const string pre = @"<root><one /><two /><three /></root>";
            const string post = @"<root><one /><name>value</name><two /><three /></root>";
            const string xpath = "root";
            var action = new AddElement {Name = "name", Value = "value", Before = "two"};
            Run(pre, post, xpath, action);
        }

        [Test]
        public void IfNoElementSelectedJustAppend()
        {
            const string pre = @"<root><list><one /><two /><three /></list><list><one /><three /></list></root>";
            const string post = @"<root><list><one /><name>value</name><two /><three /></list><list><one /><three /><name>value</name></list></root>";
            const string xpath = "root/list";
            var action = new AddElement {Name = "name", Value = "value", Before = "two"};
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AddElementAfter()
        {
            const string pre = @"<root><one /><two /><three /></root>";
            const string post = @"<root><one /><two /><name>value</name><three /></root>";
            const string xpath = "root";
            var action = new AddElement {Name = "name", Value = "value", After = "two"};
            Run(pre, post, xpath, action);
        }
    }
}