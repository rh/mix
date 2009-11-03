using Mix.Core.Exceptions;
using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class AddFragmentFixture : TestFixture
    {
        private const string Fragment = "<child>Some text</child>";
        private const string InvalidXPath = "///";

        [Test]
        public void AddFragmentAtDefaultPosition()
        {
            const string Pre = "<root />";
            const string Post = "<root>" + Fragment + "</root>";
            const string XPath = "root";
            var task = new AddFragment {Fragment = Fragment};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void AddFragmentBefore()
        {
            const string Pre = "<root><first /><last /></root>";
            const string Post = "<root>" + Fragment + "<first /><last /></root>";
            const string XPath = "root";
            var task = new AddFragment {Fragment = Fragment, Before = "first"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void InsertBeforeWithNonSelectingXPath()
        {
            const string Pre = "<root><first /><last /></root>";
            const string Post = "<root><first /><last />" + Fragment + "</root>";
            const string XPath = "root";
            var task = new AddFragment {Fragment = Fragment, Before = "non-existing"};
            Run(Pre, Post, XPath, task);
        }

        [Test, ExpectedException(typeof(TaskExecutionException))]
        public void InsertBeforeWithInvalidXPath()
        {
            const string Pre = "<root><first /><last /></root>";
            const string XPath = "root";
            var task = new AddFragment {Fragment = Fragment, Before = InvalidXPath};
            Run(Pre, null, XPath, task);
        }

        [Test]
        public void AddFragmentAfter()
        {
            const string Pre = "<root><first /><last /></root>";
            const string Post = "<root><first />" + Fragment + "<last /></root>";
            const string XPath = "root";
            var task = new AddFragment {Fragment = Fragment, After = "first"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void InsertAfterWithNonSelectingXPath()
        {
            const string Pre = "<root><first /><last /></root>";
            const string Post = "<root><first /><last />" + Fragment + "</root>";
            const string XPath = "root";
            var task = new AddFragment {Fragment = Fragment, After = "non-existing"};
            Run(Pre, Post, XPath, task);
        }

        [Test, ExpectedException(typeof(TaskExecutionException))]
        public void InsertAfterWithInvalidXPath()
        {
            const string Pre = "<root><first /><last /></root>";
            const string XPath = "root";
            var task = new AddFragment {Fragment = Fragment, After = InvalidXPath};
            Run(Pre, null, XPath, task);
        }
    }
}