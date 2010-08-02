using Mix.Exceptions;
using Mix.Tasks;
using NUnit.Framework;

namespace Mix.Tests
{
    [TestFixture]
    public class AddAttributeFixture : TestFixture
    {
        [Test]
        public void AttributeExists()
        {
            const string Pre = @"<root name="""" />";
            const string Post = Pre;
            const string XPath = "root";
            var task = new AddAttribute {Name = "name"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void SetNameOnly()
        {
            const string Pre = @"<root />";
            const string Post = @"<root post="""" />";
            const string XPath = "root";
            var task = new AddAttribute {Name = "post"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void SetNameAndValue()
        {
            const string Pre = @"<root />";
            const string Post = @"<root post=""foo"" />";
            const string XPath = "root";
            var task = new AddAttribute {Name = "post", Value = "foo"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void InsertBefore()
        {
            const string Pre = @"<root first="""" />";
            const string Post = @"<root post=""foo"" first="""" />";
            const string XPath = "root";
            var task = new AddAttribute {Name = "post", Value = "foo", Before = "@first"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void InsertBeforeWithNonSelectingInvalidXPath()
        {
            const string Pre = @"<root first="""" />";
            const string Post = @"<root first="""" post=""foo"" />";
            const string XPath = "root";
            var task = new AddAttribute {Name = "post", Value = "foo", Before = "@non-existing"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void InsertBeforeWithInvalidXPath()
        {
            const string Pre = @"<root first="""" />";
            const string XPath = "root";
            var task = new AddAttribute {Name = "post", Value = "foo", Before = "///"};

            Assert.Throws<TaskExecutionException>(() => Run(Pre, null, XPath, task));
        }

        [Test]
        public void InsertAfter()
        {
            const string Pre = @"<root first="""" second="""" />";
            const string Post = @"<root first="""" post=""foo"" second="""" />";
            const string XPath = "root";
            var task = new AddAttribute {Name = "post", Value = "foo", After = "@first"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void InsertAfterWithNonSelectingInvalidXPath()
        {
            const string Pre = @"<root first="""" second="""" />";
            const string Post = @"<root first="""" second="""" post=""foo"" />";
            const string XPath = "root";
            var task = new AddAttribute {Name = "post", Value = "foo", After = "@non-existing"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void InsertAfterWithInvalidXPath()
        {
            const string Pre = @"<root first="""" second="""" />";
            const string XPath = "root";
            var task = new AddAttribute {Name = "post", Value = "foo", After = "///"};

            Assert.Throws<TaskExecutionException>(() => Run(Pre, null, XPath, task));
        }
    }
}