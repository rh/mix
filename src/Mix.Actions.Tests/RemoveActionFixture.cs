using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class RemoveActionFixture : TestFixture
    {
        [Test]
        public void RemoveElements()
        {
            string pre = @"<root><pre /><dummy /></root>";
            string post = @"<root><dummy /></root>";
            string xpath = "root/pre";
            RemoveAction action = new RemoveAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void TryToRemoveDocumentElement()
        {
            string pre = @"<root><pre /><dummy /></root>";
            string post = pre;
            string xpath = "root";
            RemoveAction action = new RemoveAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RemoveAttribute()
        {
            string pre = @"<root pre="""" />";
            string post = @"<root />";
            string xpath = "//@pre";
            RemoveAction action = new RemoveAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RemoveAttributes()
        {
            string pre = @"<root pre=""""><node pre=""""/></root>";
            string post = @"<root><node /></root>";
            string xpath = "//@pre";
            RemoveAction action = new RemoveAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RemoveComment()
        {
            string pre = @"<root><!--COMMENT--></root>";
            string post = @"<root></root>";
            string xpath = "//comment()";
            RemoveAction action = new RemoveAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RemoveTextNodes()
        {
            string pre = @"<root>text</root>";
            string post = @"<root></root>";
            string xpath = "//text()";
            RemoveAction action = new RemoveAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RemoveCDataSections()
        {
            string pre = @"<root><![CDATA[]]></root>";
            string post = @"<root></root>";
            string xpath = "//text()";
            RemoveAction action = new RemoveAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RemoveComments()
        {
            string pre = @"<root><!--COMMENT--><node /><!--COMMENT--></root>";
            string post = @"<root><node /></root>";
            string xpath = "//comment()";
            RemoveAction action = new RemoveAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RemoveProcessingInstructions()
        {
            string pre = @"<root><?foo bar ?><node /><?foo bar ?></root>";
            string post = @"<root><node /></root>";
            string xpath = "//processing-instruction()";
            RemoveAction action = new RemoveAction();
            Run(pre, post, xpath, action);
        }
    }
}