using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class PrependFixture : TestFixture
    {
        [Test]
        public void PrependElements()
        {
            const string pre = @"<root>pre</root>";
            const string post = @"<root>prefixpre</root>";
            const string xpath = "root";
            var task = new Prepend {Value = "prefix"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void PrependNonExistingTextNode()
        {
            const string pre = @"<root></root>";
            const string post = @"<root>prefix</root>";
            const string xpath = "root";
            var task = new Prepend {Value = "prefix"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void PrependMixedElements()
        {
            const string pre = @"<root>pre<![CDATA[pre]]><?foo bar?><!--comment-->pre</root>";
            const string post = pre;
            const string xpath = "root";
            var task = new Prepend {Value = "prefix"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void PrependAttributes()
        {
            const string pre = @"<root pre=""pre"" />";
            const string post = @"<root pre=""prefixpre"" />";
            const string xpath = "root/@pre";
            var task = new Prepend {Value = "prefix"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void PrependTextNodes()
        {
            const string pre = @"<root>text</root>";
            const string post = @"<root>prefixtext</root>";
            const string xpath = "//text()";
            var task = new Prepend {Value = "prefix"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void PrependCDataSections()
        {
            const string pre = @"<root><![CDATA[text]]></root>";
            const string post = @"<root><![CDATA[prefixtext]]></root>";
            const string xpath = "//root";
            var task = new Prepend {Value = "prefix"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void PrependComments()
        {
            const string pre = @"<root><!--COMMENT--></root>";
            const string post = @"<root><!--prefixCOMMENT--></root>";
            const string xpath = "//comment()";
            var task = new Prepend {Value = "prefix"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void PrependProcessingInstructions()
        {
            const string pre = @"<root><?foo bar ?></root>";
            const string post = @"<root><?foo prefixbar ?></root>";
            const string xpath = "//processing-instruction()";
            var task = new Prepend {Value = "prefix"};
            Run(pre, post, xpath, task);
        }
    }
}