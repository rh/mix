using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class RemoveFixture : TestFixture
    {
        [Test]
        public void RemoveElements()
        {
            const string pre = @"<root><pre /><dummy /></root>";
            const string post = @"<root><dummy /></root>";
            const string xpath = "root/pre";
            var task = new Remove();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void TryToRemoveDocumentElement()
        {
            const string pre = @"<root><pre /><dummy /></root>";
            const string post = pre;
            const string xpath = "root";
            var task = new Remove();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RemoveAttribute()
        {
            const string pre = @"<root pre="""" />";
            const string post = @"<root />";
            const string xpath = "//@pre";
            var task = new Remove();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RemoveAttributes()
        {
            const string pre = @"<root pre=""""><node pre=""""/></root>";
            const string post = @"<root><node /></root>";
            const string xpath = "//@pre";
            var task = new Remove();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RemoveComment()
        {
            const string pre = @"<root><!--COMMENT--></root>";
            const string post = @"<root></root>";
            const string xpath = "//comment()";
            var task = new Remove();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RemoveTextNodes()
        {
            const string pre = @"<root>text</root>";
            const string post = @"<root></root>";
            const string xpath = "//text()";
            var task = new Remove();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RemoveCDataSections()
        {
            const string pre = @"<root><![CDATA[]]></root>";
            const string post = @"<root></root>";
            const string xpath = "//text()";
            var task = new Remove();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RemoveComments()
        {
            const string pre = @"<root><!--COMMENT--><node /><!--COMMENT--></root>";
            const string post = @"<root><node /></root>";
            const string xpath = "//comment()";
            var task = new Remove();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RemoveProcessingInstructions()
        {
            const string pre = @"<root><?foo bar ?><node /><?foo bar ?></root>";
            const string post = @"<root><node /></root>";
            const string xpath = "//processing-instruction()";
            var task = new Remove();
            Run(pre, post, xpath, task);
        }
    }
}