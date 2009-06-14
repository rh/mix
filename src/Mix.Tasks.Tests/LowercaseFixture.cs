using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class LowercaseFixture : TestFixture
    {
        [Test]
        public void LowerCaseElementsValues()
        {
            const string pre = @"<root attribute="""">PRE</root>";
            const string post = @"<root attribute="""">pre</root>";
            const string xpath = "root";
            var task = new LowerCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void LowerCaseElementsCDataValues()
        {
            const string pre = @"<root attribute=""""><![CDATA[PRE]]></root>";
            const string post = @"<root attribute=""""><![CDATA[pre]]></root>";
            const string xpath = "root";
            var task = new LowerCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void LowerCaseMixedElements()
        {
            const string pre = @"<root>A<![CDATA[VALUE]]><?foo BAR?><!--COMMENT-->B</root>";
            const string post = pre;
            const string xpath = "root";
            var task = new LowerCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void LowerCaseElementWithChildElements()
        {
            const string pre = @"<root><child><child-of-child>FOO</child-of-child></child></root>";
            const string post = pre;
            const string xpath = "root";
            var task = new LowerCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void LowerCaseAttributeValues()
        {
            const string pre = @"<root attribute=""VALUE""></root>";
            const string post = @"<root attribute=""value""></root>";
            const string xpath = "root/@attribute";
            var task = new LowerCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void LowerCaseTextNodes()
        {
            const string pre = @"<root>TEXT</root>";
            const string post = @"<root>text</root>";
            const string xpath = "//text()";
            var task = new LowerCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void LowerCaseCDataSections()
        {
            const string pre = @"<root><![CDATA[SOMETHING]]></root>";
            const string post = @"<root><![CDATA[something]]></root>";
            const string xpath = "//root";
            var task = new LowerCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void LowerCaseComments()
        {
            const string pre = @"<root><!--COMMENT--></root>";
            const string post = @"<root><!--comment--></root>";
            const string xpath = "//comment()";
            var task = new LowerCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void LowerCaseProcessingInstructions()
        {
            const string pre = @"<root><?foo BAR ?></root>";
            const string post = @"<root><?foo bar ?></root>";
            const string xpath = "//processing-instruction()";
            var task = new LowerCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void LowerCaseTextNodesWithRegularExpression()
        {
            const string pre = @"<root>TEXT TEXT</root>";
            const string post = @"<root>tEXT tEXT</root>";
            const string xpath = "//text()";
            var task = new LowerCase {Pattern = @"\b[A-Z]{1}"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void LowerCaseTextNodesWithSimpleRegularExpression()
        {
            const string pre = @"<root>ABCDEFGH</root>";
            const string post = @"<root>ABcdeFGH</root>";
            const string xpath = "//text()";
            var task = new LowerCase {Pattern = "CDE"};
            Run(pre, post, xpath, task);
        }
    }
}