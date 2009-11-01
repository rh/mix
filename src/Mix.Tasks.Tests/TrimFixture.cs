using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class TrimFixture : TestFixture
    {
        [Test]
        public void Elements()
        {
            const string pre = @"<root><node> some text  </node></root>";
            const string post = @"<root><node>some text</node></root>";
            const string xpath = "//node";
            var task = new Trim();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ElementWithMixedContent()
        {
            const string pre = @"<root a=""  foo   ""><![CDATA[ text  ]]></root>";
            const string post = @"<root a=""  foo   ""><![CDATA[text]]></root>";
            const string xpath = "root";
            var task = new Trim();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ElementWithChildElements()
        {
            const string pre = @"<root><child><child-of-child>  foo    </child-of-child></child></root>";
            const string post = pre;
            const string xpath = "root";
            var task = new Trim();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void Attributes()
        {
            const string pre = @"<root attribute="" some text  ""></root>";
            const string post = @"<root attribute=""some text""></root>";
            const string xpath = "//@*";
            var task = new Trim();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void TextNodes()
        {
            const string pre = @"<root> text  </root>";
            const string post = @"<root>text</root>";
            const string xpath = "//text()";
            var task = new Trim();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CDataSections()
        {
            const string pre = @"<root><![CDATA[ text  ]]></root>";
            const string post = @"<root><![CDATA[text]]></root>";
            const string xpath = "//text()";
            var task = new Trim();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void Comments()
        {
            const string pre = @"<root><!-- COMMENT  --><!--   COMMENT --></root>";
            const string post = @"<root><!--COMMENT--><!--COMMENT--></root>";
            const string xpath = "//comment()";
            var task = new Trim();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ProcessingInstructions()
        {
            const string pre = @"<root><?foo     bar  ?><?foo  bar ?></root>";
            const string post = @"<root><?foo bar?><?foo bar?></root>";
            const string xpath = "//processing-instruction()";
            var task = new Trim();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void TrimElementsWithRegularExpression()
        {
            const string pre = @"<root><node>Line 1 . Line 2 . Line 3 .</node></root>";
            const string post = @"<root><node>Line 1. Line 2. Line 3.</node></root>";
            const string xpath = "//node";
            var task = new Trim {Pattern = @"\s*\."};
            Run(pre, post, xpath, task);
        }
    }
}