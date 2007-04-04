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
        public void UpperCaseAttributeValues()
        {
            string pre = @"<root attribute=""value""></root>";
            string post = @"<root attribute=""VALUE""></root>";
            string xpath = "root/@attribute";
            UpperCaseAction action = new UpperCaseAction();
            Run(pre, post, xpath, action);
        }
    }
}