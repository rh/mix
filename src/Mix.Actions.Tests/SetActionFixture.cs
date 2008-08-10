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
            Set action = new Set();
            action.Value = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void NormalText()
        {
            string pre = @"<root></root>";
            string post = @"<root>post</root>";
            string xpath = "root";
            Set action = new Set();
            action.Value = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void SpecialText()
        {
            string pre = @"<root></root>";
            string post = @"<root>&gt;</root>";
            string xpath = "root";
            Set action = new Set();
            action.Value = ">";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ElementWithMixedChildNodes()
        {
            string pre = @"<root>pre<![CDATA[pre]]>pre</root>";
            string post = @"<root>post</root>";
            string xpath = "root";
            Set action = new Set();
            action.Value = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ElementWithoutChildNodes()
        {
            string pre = @"<root></root>";
            string post = @"<root>post</root>";
            string xpath = "root";
            Set action = new Set();
            action.Value = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Attribute()
        {
            string pre = @"<root attribute=""""></root>";
            string post = @"<root attribute=""value""></root>";
            string xpath = "root/@attribute";
            Set action = new Set();
            action.Value = "value";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Attributes()
        {
            string pre = @"<root a="""" b="""" c=""""></root>";
            string post = @"<root a=""value"" b=""value"" c=""value""></root>";
            string xpath = "//@*";
            Set action = new Set();
            action.Value = "value";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void TextNodes()
        {
            string pre = @"<root>text</root>";
            string post = @"<root>value</root>";
            string xpath = "//text()";
            Set action = new Set();
            action.Value = "value";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void CDataSections()
        {
            string pre = @"<root><![CDATA[text]]></root>";
            string post = @"<root><![CDATA[value]]></root>";
            string xpath = "//text()";
            Set action = new Set();
            action.Value = "value";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Comments()
        {
            string pre = @"<root><!--COMMENT--><!--COMMENT--></root>";
            string post = @"<root><!--value--><!--value--></root>";
            string xpath = "//comment()";
            Set action = new Set();
            action.Value = "value";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ProcessingInstructions()
        {
            string pre = @"<root><?foo bar?><?foo bar?></root>";
            string post = @"<root><?foo value?><?foo value?></root>";
            string xpath = "//processing-instruction()";
            Set action = new Set();
            action.Value = "value";
            Run(pre, post, xpath, action);
        }
    }
}