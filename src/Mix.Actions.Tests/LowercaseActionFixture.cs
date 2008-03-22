using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class LowercaseActionFixture : TestFixture
    {
        [Test]
        public void LowerCaseElementsValues()
        {
            string pre = @"<root attribute="""">PRE</root>";
            string post = @"<root attribute="""">pre</root>";
            string xpath = "root";
            LowerCaseAction action = new LowerCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void LowerCaseElementsCDataValues()
        {
            string pre = @"<root attribute=""""><![CDATA[PRE]]></root>";
            string post = @"<root attribute=""""><![CDATA[pre]]></root>";
            string xpath = "root";
            LowerCaseAction action = new LowerCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void LowerCaseMixedElements()
        {
            string pre = @"<root>A<![CDATA[VALUE]]><?foo BAR?><!--COMMENT-->B</root>";
            string post = @"<root>a<![CDATA[value]]><?foo bar?><!--comment-->b</root>";
            string xpath = "root";
            LowerCaseAction action = new LowerCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void LowerCaseAttributeValues()
        {
            string pre = @"<root attribute=""VALUE""></root>";
            string post = @"<root attribute=""value""></root>";
            string xpath = "root/@attribute";
            LowerCaseAction action = new LowerCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void LowerCaseTextNodes()
        {
            string pre = @"<root>TEXT</root>";
            string post = @"<root>text</root>";
            string xpath = "//text()";
            LowerCaseAction action = new LowerCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void LowerCaseCDataSections()
        {
            string pre = @"<root><![CDATA[SOMETHING]]></root>";
            string post = @"<root><![CDATA[something]]></root>";
            string xpath = "//root";
            LowerCaseAction action = new LowerCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void LowerCaseComments()
        {
            string pre = @"<root><!--COMMENT--></root>";
            string post = @"<root><!--comment--></root>";
            string xpath = "//comment()";
            LowerCaseAction action = new LowerCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void LowerCaseProcessingInstructions()
        {
            string pre = @"<root><?foo BAR ?></root>";
            string post = @"<root><?foo bar ?></root>";
            string xpath = "//processing-instruction()";
            LowerCaseAction action = new LowerCaseAction();
            Run(pre, post, xpath, action);
        }
    }
}