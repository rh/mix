using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class ClearFixture : TestFixture
    {
        [Test]
        public void ClearElements()
        {
            const string pre = @"<root>something</root>";
            const string post = @"<root></root>";
            const string xpath = "root";
            var task = new Clear();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ClearMixedElements()
        {
            const string pre = @"<root a=""foo"">something<![CDATA[something]]><?foo bar?><!--comment-->something</root>";
            const string post = @"<root a=""foo""></root>";
            const string xpath = "root";
            var task = new Clear();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ClearElementWithChildElements()
        {
            const string pre = @"<root><child><child-of-child>foo</child-of-child></child></root>";
            const string post = @"<root></root>";
            const string xpath = "root";
            var task = new Clear();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ClearAttribute()
        {
            const string pre = @"<root pre=""something"" />";
            const string post = @"<root pre="""" />";
            const string xpath = "//@pre";
            var task = new Clear();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ClearAttributes()
        {
            const string pre = @"<root pre=""something""><node pre=""something""/></root>";
            const string post = @"<root pre=""""><node pre="""" /></root>";
            const string xpath = "//@pre";
            var task = new Clear();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ClearTextNode()
        {
            const string pre = @"<root>text</root>";
            const string post = @"<root></root>";
            const string xpath = "//text()";
            var task = new Clear();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ClearComment()
        {
            const string pre = @"<root><!--COMMENT--></root>";
            const string post = @"<root><!----></root>";
            const string xpath = "//comment()";
            var task = new Clear();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ClearComments()
        {
            const string pre = @"<root><!--COMMENT--><!--COMMENT--></root>";
            const string post = @"<root><!----><!----></root>";
            const string xpath = "//comment()";
            var task = new Clear();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ClearProcessingInstructions()
        {
            const string pre = @"<root><?foo bar ?><?foo bar ?></root>";
            const string post = @"<root><?foo ?><?foo ?></root>";
            const string xpath = "//processing-instruction()";
            var task = new Clear();
            Run(pre, post, xpath, task);
        }
    }
}