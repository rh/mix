using Mix.Tasks;
using NUnit.Framework;

namespace Mix.Tests
{
    [TestFixture]
    public class SetFixture : TestFixture
    {
        [Test]
        public void ElementWithOneTextNode()
        {
            const string pre = @"<root>pre</root>";
            const string post = @"<root>post</root>";
            const string xpath = "root";
            var task = new Set {Value = "post"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void NormalText()
        {
            const string pre = @"<root></root>";
            const string post = @"<root>post</root>";
            const string xpath = "root";
            var task = new Set {Value = "post"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void SpecialText()
        {
            const string pre = @"<root></root>";
            const string post = @"<root>&gt;</root>";
            const string xpath = "root";
            var task = new Set {Value = ">"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ElementWithMixedChildNodes()
        {
            const string pre = @"<root>pre<![CDATA[pre]]>pre</root>";
            const string post = @"<root>post</root>";
            const string xpath = "root";
            var task = new Set {Value = "post"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ElementWithoutChildNodes()
        {
            const string pre = @"<root></root>";
            const string post = @"<root>post</root>";
            const string xpath = "root";
            var task = new Set {Value = "post"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void Attribute()
        {
            const string pre = @"<root attribute=""""></root>";
            const string post = @"<root attribute=""value""></root>";
            const string xpath = "root/@attribute";
            var task = new Set {Value = "value"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void Attributes()
        {
            const string pre = @"<root a="""" b="""" c=""""></root>";
            const string post = @"<root a=""value"" b=""value"" c=""value""></root>";
            const string xpath = "//@*";
            var task = new Set {Value = "value"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void TextNodes()
        {
            const string pre = @"<root>text</root>";
            const string post = @"<root>value</root>";
            const string xpath = "//text()";
            var task = new Set {Value = "value"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CDataSections()
        {
            const string pre = @"<root><![CDATA[text]]></root>";
            const string post = @"<root><![CDATA[value]]></root>";
            const string xpath = "//text()";
            var task = new Set {Value = "value"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void Comments()
        {
            const string pre = @"<root><!--COMMENT--><!--COMMENT--></root>";
            const string post = @"<root><!--value--><!--value--></root>";
            const string xpath = "//comment()";
            var task = new Set {Value = "value"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ProcessingInstructions()
        {
            const string pre = @"<root><?foo bar?><?foo bar?></root>";
            const string post = @"<root><?foo value?><?foo value?></root>";
            const string xpath = "//processing-instruction()";
            var task = new Set {Value = "value"};
            Run(pre, post, xpath, task);
        }
    }
}