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
            string pre = @"<root>a<![CDATA[value]]>b</root>";
            string post = @"<root>A<![CDATA[VALUE]]>B</root>";
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
        public void UpperCaseComments()
        {
            string pre = @"<root><!--comment--></root>";
            string post = @"<root><!--COMMENT--></root>";
            string xpath = "//comment()";
            UpperCaseAction action = new UpperCaseAction();
            Run(pre, post, xpath, action);
        }
    }
}