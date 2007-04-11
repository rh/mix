using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class SetActionFixture : TestFixture
    {
        [Test]
        public void ElementWithOneTextNode()
        {
            string pre = @"<root>pre</root>";
            string post = @"<root>post</root>";
            string xpath = "root";
            SetAction action = new SetAction();
            action.Text = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ElementWithOneCDataSection()
        {
            string pre = @"<root><![CDATA[pre]]></root>";
            string post = @"<root><![CDATA[post]]></root>";
            string xpath = "root";
            SetAction action = new SetAction();
            action.Text = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ElementWithMixedChildNodes()
        {
            string pre = @"<root>pre<![CDATA[pre]]>pre</root>";
            string post = @"<root>post</root>";
            string xpath = "root";
            SetAction action = new SetAction();
            action.Text = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ElementWithoutChildNodes()
        {
            string pre = @"<root></root>";
            string post = @"<root>post</root>";
            string xpath = "root";
            SetAction action = new SetAction();
            action.Text = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Attribute()
        {
            string pre = @"<root attribute=""""></root>";
            string post = @"<root attribute=""value""></root>";
            string xpath = "root/@attribute";
            SetAction action = new SetAction();
            action.Text = "value";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Attributes()
        {
            string pre = @"<root a="""" b="""" c=""""></root>";
            string post = @"<root a=""value"" b=""value"" c=""value""></root>";
            string xpath = "//@*";
            SetAction action = new SetAction();
            action.Text = "value";
            Run(pre, post, xpath, action);
        }
    }
}