using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class PrependActionFixture : TestFixture
    {
        [Test]
        public void PrependElements()
        {
            string pre = @"<root>pre</root>";
            string post = @"<root>prefixpre</root>";
            string xpath = "root";
            PrependAction action = new PrependAction();
            action.Value = "prefix";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void PrependMixedElements()
        {
            string pre = @"<root>pre<![CDATA[pre]]>pre</root>";
            string post = @"<root>prefixpre<![CDATA[prefixpre]]>prefixpre</root>";
            string xpath = "root";
            PrependAction action = new PrependAction();
            action.Value = "prefix";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void PrependAttributes()
        {
            string pre = @"<root pre=""pre"" />";
            string post = @"<root pre=""prefixpre"" />";
            string xpath = "root/@pre";
            PrependAction action = new PrependAction();
            action.Value = "prefix";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void PrependTextNodes()
        {
            string pre = @"<root>text</root>";
            string post = @"<root>prefixtext</root>";
            string xpath = "//text()";
            PrependAction action = new PrependAction();
            action.Value = "prefix";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void PrependCDataSections()
        {
            string pre = @"<root><![CDATA[text]]></root>";
            string post = @"<root><![CDATA[prefixtext]]></root>";
            string xpath = "//root";
            PrependAction action = new PrependAction();
            action.Value = "prefix";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void PrependComments()
        {
            string pre = @"<root><!--COMMENT--></root>";
            string post = @"<root><!--prefixCOMMENT--></root>";
            string xpath = "//comment()";
            PrependAction action = new PrependAction();
            action.Value = "prefix";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void PrependProcessingInstructions()
        {
            string pre = @"<root><?foo bar ?></root>";
            string post = @"<root><?foo prefixbar ?></root>";
            string xpath = "//processing-instruction()";
            PrependAction action = new PrependAction();
            action.Value = "prefix";
            Run(pre, post, xpath, action);
        }
    }
}