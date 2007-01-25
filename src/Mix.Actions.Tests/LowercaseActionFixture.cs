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
        public void LowerCaseAttributes()
        {
            string pre = @"<root ATTRIBUTE=""""></root>";
            string post = @"<root attribute=""""></root>";
            string xpath = "root/@ATTRIBUTE";
            Action action = new LowerCaseAction();
            action.ActionType = ActionType.Property;
            Run(pre, post, xpath, action);
        }

        [Test]
        public void LowerCaseAttributesConsideringPosition1()
        {
            string pre = @"<root ATTRIBUTE1="""" ATTRIBUTE2="""" ATTRIBUTE3=""""></root>";
            string post = @"<root ATTRIBUTE1="""" attribute2="""" ATTRIBUTE3=""""></root>";
            string xpath = "root/@ATTRIBUTE2";
            Action action = new LowerCaseAction();
            action.ActionType = ActionType.Property;
            Run(pre, post, xpath, action);
        }

        [Test]
        public void LowerCaseAttributesConsideringPosition2()
        {
            string pre = @"<root ATTRIBUTE1="""" ATTRIBUTE2="""" ATTRIBUTE3=""""></root>";
            string post = @"<root attribute1="""" ATTRIBUTE2="""" ATTRIBUTE3=""""></root>";
            string xpath = "root/@ATTRIBUTE1";
            Action action = new LowerCaseAction();
            action.ActionType = ActionType.Property;
            Run(pre, post, xpath, action);
        }

        [Test]
        public void LowerCaseAttributesConsideringPosition3()
        {
            string pre = @"<root ATTRIBUTE1="""" ATTRIBUTE2="""" ATTRIBUTE3=""""></root>";
            string post = @"<root ATTRIBUTE1="""" ATTRIBUTE2="""" attribute3=""""></root>";
            string xpath = "root/@ATTRIBUTE3";
            Action action = new LowerCaseAction();
            action.ActionType = ActionType.Property;
            Run(pre, post, xpath, action);
        }

        [Test]
        public void LowerCaseExistingAttributes()
        {
            string pre = @"<root attribute="""" ATTRIBUTE=""""></root>";
            string post = @"<root attribute="""" ATTRIBUTE=""""></root>";
            string xpath = "root/@ATTRIBUTE";
            Action action = new LowerCaseAction();
            action.ActionType = ActionType.Property;
            Run(pre, post, xpath, action);
        }

        [Test]
        public void LowerCaseAttributeValues()
        {
            string pre = @"<root attribute=""VALUE""></root>";
            string post = @"<root attribute=""value""></root>";
            string xpath = "root/@attribute";
            Action action = new LowerCaseAction();
            action.ActionType = ActionType.Value;
            Run(pre, post, xpath, action);
        }
    }
}
