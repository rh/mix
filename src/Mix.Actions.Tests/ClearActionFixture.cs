using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class ClearActionFixture : TestFixture
    {
        [Test]
        public void ClearElements()
        {
            string pre = @"<root>something</root>";
            string post = @"<root></root>";
            string xpath = "root";
            ClearAction action = new ClearAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ClearMixedElements()
        {
            string pre = @"<root a=""foo"">something<![CDATA[something]]><?foo bar?>something</root>";
            string post = @"<root a=""""><![CDATA[]]><?foo ?></root>";
            string xpath = "root";
            ClearAction action = new ClearAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ClearAttribute()
        {
            string pre = @"<root pre=""something"" />";
            string post = @"<root pre="""" />";
            string xpath = "//@pre";
            ClearAction action = new ClearAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ClearAttributes()
        {
            string pre = @"<root pre=""something""><node pre=""something""/></root>";
            string post = @"<root pre=""""><node pre="""" /></root>";
            string xpath = "//@pre";
            ClearAction action = new ClearAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ClearTextNode()
        {
            string pre = @"<root>text</root>";
            string post = @"<root></root>";
            string xpath = "//text()";
            ClearAction action = new ClearAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ClearComment()
        {
            string pre = @"<root><!--COMMENT--></root>";
            string post = @"<root><!----></root>";
            string xpath = "//comment()";
            ClearAction action = new ClearAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ClearComments()
        {
            string pre = @"<root><!--COMMENT--><!--COMMENT--></root>";
            string post = @"<root><!----><!----></root>";
            string xpath = "//comment()";
            ClearAction action = new ClearAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ClearProcessingInstructions()
        {
            string pre = @"<root><?foo bar ?><?foo bar ?></root>";
            string post = @"<root><?foo ?><?foo ?></root>";
            string xpath = "//processing-instruction()";
            ClearAction action = new ClearAction();
            Run(pre, post, xpath, action);
        }
    }
}