using Mix.Core.Exceptions;
using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class AddElementFixture : TestFixture
    {
        [Test]
        public void AddElementWithoutValue()
        {
            const string Pre = @"<root />";
            const string Post = @"<root><name></name></root>";
            const string XPath = "root";
            var task = new AddElement {Name = "name"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void AddElementWithValue()
        {
            const string Pre = @"<root />";
            const string Post = @"<root><name>value</name></root>";
            const string XPath = "root";
            var task = new AddElement {Name = "name", Value = "value"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void AddElementBefore()
        {
            const string Pre = @"<root><one /><two /><three /></root>";
            const string Post = @"<root><one /><name>value</name><two /><three /></root>";
            const string XPath = "root";
            var task = new AddElement {Name = "name", Value = "value", Before = "two"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void AddElementBeforeWithNonSelectingXPath()
        {
            const string Pre = @"<root><one /><two /><three /></root>";
            const string Post = @"<root><one /><two /><three /><name>value</name></root>";
            const string XPath = "root";
            var task = new AddElement {Name = "name", Value = "value", Before = "non-existing"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void IfNoElementSelectedJustAppend()
        {
            const string Pre = @"<root><list><one /><two /><three /></list><list><one /><three /></list></root>";
            const string Post = @"<root><list><one /><name>value</name><two /><three /></list><list><one /><three /><name>value</name></list></root>";
            const string XPath = "root/list";
            var task = new AddElement {Name = "name", Value = "value", Before = "two"};
            Run(Pre, Post, XPath, task);
        }

        [Test, ExpectedException(typeof(TaskExecutionException))]
        public void AddElementBeforeWithInvalidXPath()
        {
            const string Pre = @"<root />";
            const string XPath = "root";
            var task = new AddElement {Name = "name", Value = "value", Before = "///"};
            Run(Pre, null, XPath, task);
        }

        [Test]
        public void AddElementAfter()
        {
            const string Pre = @"<root><one /><two /><three /></root>";
            const string Post = @"<root><one /><two /><name>value</name><three /></root>";
            const string XPath = "root";
            var task = new AddElement {Name = "name", Value = "value", After = "two"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void AddElementAfterWithNonSelectingXPath()
        {
            const string Pre = @"<root><one /><two /><three /></root>";
            const string Post = @"<root><one /><two /><three /><name>value</name></root>";
            const string XPath = "root";
            var task = new AddElement {Name = "name", Value = "value", After = "non-existing"};
            Run(Pre, Post, XPath, task);
        }

        [Test, ExpectedException(typeof(TaskExecutionException))]
        public void AddElementAfterWithInvalidXPath()
        {
            const string Pre = @"<root />";
            const string XPath = "root";
            var task = new AddElement {Name = "name", Value = "value", After = "///"};
            Run(Pre, null, XPath, task);
        }
    }
}