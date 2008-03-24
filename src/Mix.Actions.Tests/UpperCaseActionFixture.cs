using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class UpperCaseActionFixture : TestFixture
    {
        [Test]
        public void UpperCaseElementsValues()
        {
            string pre = @"<root attribute="""">value</root>";
            string post = @"<root attribute="""">VALUE</root>";
            string xpath = "root";
            UpperCaseAction action = new UpperCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void UpperCaseElementsCDataValues()
        {
            string pre = @"<root attribute=""""><![CDATA[value]]></root>";
            string post = @"<root attribute=""""><![CDATA[VALUE]]></root>";
            string xpath = "root";
            UpperCaseAction action = new UpperCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void UpperCaseMixedElements()
        {
            string pre = @"<root a=""a"">a<![CDATA[value]]><?foo bar?><!--comment-->b</root>";
            string post = @"<root a=""A"">A<![CDATA[VALUE]]><?foo BAR?><!--COMMENT-->B</root>";
            string xpath = "root";
            UpperCaseAction action = new UpperCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void UpperCaseElementRecursively()
        {
            string pre = @"<root><child><child-of-child>foo</child-of-child></child></root>";
            string post = @"<root><child><child-of-child>FOO</child-of-child></child></root>";
            string xpath = "root";
            UpperCaseAction action = new UpperCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void UpperCaseAttributeValues()
        {
            string pre = @"<root attribute=""value""></root>";
            string post = @"<root attribute=""VALUE""></root>";
            string xpath = "root/@attribute";
            UpperCaseAction action = new UpperCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void UpperCaseTextNodes()
        {
            string pre = @"<root>text</root>";
            string post = @"<root>TEXT</root>";
            string xpath = "//text()";
            UpperCaseAction action = new UpperCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void UpperCaseCDataSections()
        {
            string pre = @"<root><![CDATA[text]]></root>";
            string post = @"<root><![CDATA[TEXT]]></root>";
            string xpath = "//text()";
            UpperCaseAction action = new UpperCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void UpperCaseComments()
        {
            string pre = @"<root><!--comment--></root>";
            string post = @"<root><!--COMMENT--></root>";
            string xpath = "//comment()";
            UpperCaseAction action = new UpperCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void UpperCaseProcessingInstructions()
        {
            string pre = @"<root><?foo bar ?></root>";
            string post = @"<root><?foo BAR ?></root>";
            string xpath = "//processing-instruction()";
            UpperCaseAction action = new UpperCaseAction();
            Run(pre, post, xpath, action);
        }
    }
}