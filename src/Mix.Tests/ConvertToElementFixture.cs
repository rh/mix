using Mix.Exceptions;
using Mix.Tasks;
using NUnit.Framework;

namespace Mix.Tests
{
    [TestFixture]
    public class ConvertToElementFixture : TestFixture
    {
        [Test]
        public void ElementNotYetPresent()
        {
            const string pre = @"<root foo=""bar"" />";
            const string post = @"<root><foo>bar</foo></root>";
            const string xpath = "//@foo";
            var task = new ConvertToElement();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ElementAlreadyPresent()
        {
            const string pre = @"<root foo=""bar""><foo>bar</foo></root>";
            const string post = @"<root><foo>bar</foo><foo>bar</foo></root>";
            const string xpath = "//@foo";
            var task = new ConvertToElement();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void InsertAsFirstChild()
        {
            const string pre = @"<root foo=""bar""><bar>baz</bar></root>";
            const string post = @"<root><foo>bar</foo><bar>baz</bar></root>";
            const string xpath = "//@foo";
            var task = new ConvertToElement();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void NameShouldBeSuppliedForText()
        {
            const string pre = @"<root>foo</root>";
            const string xpath = "//text()";
            var task = new ConvertToElement();

            Assert.Throws<RequirementException>(() => Run(pre, null, xpath, task));
        }

        [Test]
        public void NameShouldBeSuppliedForCData()
        {
            const string pre = @"<root><![CDATA[text]]></root>";
            const string xpath = "//text()";
            var task = new ConvertToElement();

            Assert.Throws<RequirementException>(() => Run(pre, null, xpath, task));
        }

        [Test]
        public void NameShouldBeSuppliedForComments()
        {
            const string pre = @"<root><!----></root>";
            const string xpath = "//comment()";
            var task = new ConvertToElement();

            Assert.Throws<RequirementException>(() => Run(pre, null, xpath, task));
        }

        [Test]
        public void Text()
        {
            const string pre = @"<root>foo</root>";
            const string post = @"<root><foo>foo</foo></root>";
            const string xpath = "//text()";
            var task = new ConvertToElement {Name = "foo"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CData()
        {
            const string pre = @"<root><![CDATA[text]]></root>";
            const string post = @"<root><foo>text</foo></root>";
            const string xpath = "//text()";
            var task = new ConvertToElement {Name = "foo"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void Comment()
        {
            const string pre = @"<root><!--comment--></root>";
            const string post = @"<root><converted>comment</converted></root>";
            const string xpath = "//comment()";
            var task = new ConvertToElement {Name = "converted"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CommentsAtRootShouldNotBeConverted()
        {
            const string pre = @"<!--comment--><root></root>";
            const string post = pre;
            const string xpath = "//comment()";
            var task = new ConvertToElement {Name = "converted"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ProcessingInstruction()
        {
            const string pre = @"<root><?pi foo?></root>";
            const string post = @"<root><pi>foo</pi></root>";
            const string xpath = "//processing-instruction()";
            var task = new ConvertToElement();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ProcessingInstructionsAtRootShouldNotBeConverted()
        {
            const string pre = @"<?pi foo?><root></root>";
            const string post = pre;
            const string xpath = "//processing-instruction()";
            var task = new ConvertToElement();
            Run(pre, post, xpath, task);
        }
    }
}