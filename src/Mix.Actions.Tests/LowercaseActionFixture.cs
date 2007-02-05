using Mix.Core;
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
            Action action = new LowerCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void LowerCaseElementsCDataValues()
        {
            string pre = @"<root attribute=""""><![CDATA[PRE]]></root>";
            string post = @"<root attribute=""""><![CDATA[pre]]></root>";
            string xpath = "root";
            Action action = new LowerCaseAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void LowerCaseAttributeValues()
        {
            string pre = @"<root attribute=""VALUE""></root>";
            string post = @"<root attribute=""value""></root>";
            string xpath = "root/@attribute";
            Action action = new LowerCaseAction();
            Run(pre, post, xpath, action);
        }
    }
}
