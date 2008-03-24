using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class TrimActionFixture : TestFixture
    {
        [Test]
        public void Elements()
        {
            string pre = @"<root><node> some text  </node></root>";
            string post = @"<root><node>some text</node></root>";
            string xpath = "//node";
            TrimAction action = new TrimAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ElementWithMixedContent()
        {
            string pre = @"<root a=""  foo   ""><node></node><![CDATA[ text  ]]><?foo    bar  ?><!-- comment   --></root>";
            string post = @"<root a=""foo""><node></node><![CDATA[text]]><?foo bar?><!--comment--></root>";
            string xpath = "root";
            TrimAction action = new TrimAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ElementWithChildElements()
        {
            string pre = @"<root><child><child-of-child>  foo    </child-of-child></child></root>";
            string post = @"<root><child><child-of-child>foo</child-of-child></child></root>";
            string xpath = "root";
            TrimAction action = new TrimAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Attributes()
        {
            string pre = @"<root attribute="" some text  ""></root>";
            string post = @"<root attribute=""some text""></root>";
            string xpath = "//@*";
            TrimAction action = new TrimAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void TextNodes()
        {
            string pre = @"<root> text  </root>";
            string post = @"<root>text</root>";
            string xpath = "//text()";
            TrimAction action = new TrimAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void CDataSections()
        {
            string pre = @"<root><![CDATA[ text  ]]></root>";
            string post = @"<root><![CDATA[text]]></root>";
            string xpath = "//text()";
            TrimAction action = new TrimAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Comments()
        {
            string pre = @"<root><!-- COMMENT  --><!--   COMMENT --></root>";
            string post = @"<root><!--COMMENT--><!--COMMENT--></root>";
            string xpath = "//comment()";
            TrimAction action = new TrimAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ProcessingInstructions()
        {
            string pre = @"<root><?foo     bar  ?><?foo  bar ?></root>";
            string post = @"<root><?foo bar?><?foo bar?></root>";
            string xpath = "//processing-instruction()";
            TrimAction action = new TrimAction();
            Run(pre, post, xpath, action);
        }
    }
}