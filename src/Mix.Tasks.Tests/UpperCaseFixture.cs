using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class UpperCaseFixture : TestFixture
    {
        [Test]
        public void UpperCaseElementsValues()
        {
            const string pre = @"<root attribute="""">value</root>";
            const string post = @"<root attribute="""">VALUE</root>";
            const string xpath = "root";
            var task = new UpperCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void UpperCaseElementsCDataValues()
        {
            const string pre = @"<root attribute=""""><![CDATA[value]]></root>";
            const string post = @"<root attribute=""""><![CDATA[VALUE]]></root>";
            const string xpath = "root";
            var task = new UpperCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void UpperCaseMixedElements()
        {
            const string pre = @"<root a=""a"">a<![CDATA[value]]><?foo bar?><!--comment-->b</root>";
            const string post = pre;
            const string xpath = "root";
            var task = new UpperCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void UpperCaseElementRecursively()
        {
            const string pre = @"<root><child><child-of-child>foo</child-of-child></child></root>";
            const string post = pre;
            const string xpath = "root";
            var task = new UpperCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void UpperCaseAttributeValues()
        {
            const string pre = @"<root attribute=""value""></root>";
            const string post = @"<root attribute=""VALUE""></root>";
            const string xpath = "root/@attribute";
            var task = new UpperCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void UpperCaseTextNodes()
        {
            const string pre = @"<root>text</root>";
            const string post = @"<root>TEXT</root>";
            const string xpath = "//text()";
            var task = new UpperCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void UpperCaseCDataSections()
        {
            const string pre = @"<root><![CDATA[text]]></root>";
            const string post = @"<root><![CDATA[TEXT]]></root>";
            const string xpath = "//text()";
            var task = new UpperCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void UpperCaseComments()
        {
            const string pre = @"<root><!--comment--></root>";
            const string post = @"<root><!--COMMENT--></root>";
            const string xpath = "//comment()";
            var task = new UpperCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void UpperCaseProcessingInstructions()
        {
            const string pre = @"<root><?foo bar ?></root>";
            const string post = @"<root><?foo BAR ?></root>";
            const string xpath = "//processing-instruction()";
            var task = new UpperCase();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void UpperCaseTextNodesWithRegularExpression()
        {
            const string pre = @"<root>text text</root>";
            const string post = @"<root>Text Text</root>";
            const string xpath = "//text()";
            var task = new UpperCase {Pattern = @"\b[a-z]{1}"};
            Run(pre, post, xpath, task);
        }
    }
}