using Mix.Core.Exceptions;
using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class ConvertToAttributeFixture : TestFixture
    {
        [Test]
        public void ElementIsTextNode()
        {
            const string pre = @"<root><foo>bar</foo></root>";
            const string post = @"<root foo=""bar""></root>";
            const string xpath = "//foo";
            var task = new ConvertToAttribute();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ElementIsNotTextNode()
        {
            const string pre = @"<root><foo><bar />bar</foo></root>";
            const string post = pre;
            const string xpath = "//foo";
            var task = new ConvertToAttribute();
            Run(pre, post, xpath, task);
        }

        [Test, ExpectedException(typeof(RequirementException))]
        public void NameShouldBeSuppliedForText()
        {
            const string pre = @"<root>foo</root>";
            const string xpath = "//text()";
            var task = new ConvertToAttribute();
            Run(pre, null, xpath, task);
        }

        [Test, ExpectedException(typeof(RequirementException))]
        public void NameShouldBeSuppliedForCData()
        {
            const string pre = @"<root><![CDATA[text]]></root>";
            const string xpath = "//text()";
            var task = new ConvertToAttribute();
            Run(pre, null, xpath, task);
        }

        [Test, ExpectedException(typeof(RequirementException))]
        public void NameShouldBeSuppliedForComments()
        {
            const string pre = @"<root><!----></root>";
            const string xpath = "//comment()";
            var task = new ConvertToAttribute();
            Run(pre, null, xpath, task);
        }

        [Test]
        public void Text()
        {
            const string pre = @"<root>text</root>";
            const string post = @"<root foo=""text""></root>";
            const string xpath = "//text()";
            var task = new ConvertToAttribute {Name = "foo"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void TextShouldNotBeConvertedWhenTheNamedAttributeAlreadyExists()
        {
            const string pre = @"<root foo="""">text</root>";
            const string post = pre;
            const string xpath = "//text()";
            var task = new ConvertToAttribute {Name = "foo"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CData()
        {
            const string pre = @"<root><![CDATA[text]]></root>";
            const string post = @"<root foo=""text""></root>";
            const string xpath = "//text()";
            var task = new ConvertToAttribute {Name = "foo"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CDataShouldNotBeConvertedWhenTheNamedAttributeAlreadyExists()
        {
            const string pre = @"<root foo=""""><![CDATA[text]]></root>";
            const string post = pre;
            const string xpath = "//text()";
            var task = new ConvertToAttribute {Name = "foo"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void Comment()
        {
            const string pre = @"<root><!--text--></root>";
            const string post = @"<root foo=""text""></root>";
            const string xpath = "//comment()";
            var task = new ConvertToAttribute {Name = "foo"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CommentShouldNotBeConvertedWhenTheNamedAttributeAlreadyExists()
        {
            const string pre = @"<root foo=""""><!--text--></root>";
            const string post = pre;
            const string xpath = "//comment()";
            var task = new ConvertToAttribute {Name = "foo"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ProcessingInstruction()
        {
            const string pre = @"<root><?pi text?></root>";
            const string post = @"<root pi=""text""></root>";
            const string xpath = "//processing-instruction()";
            var task = new ConvertToAttribute();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ProcessingInstructionShouldNotBeConvertedWhenTheNamedAttributeAlreadyExists()
        {
            const string pre = @"<root pi=""""><?pi text?></root>";
            const string post = pre;
            const string xpath = "//processing-instruction()";
            var task = new ConvertToAttribute();
            Run(pre, post, xpath, task);
        }
    }
}