using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class ConvertToTextFixture : TestFixture
    {
        [Test]
        public void ConvertElementToText()
        {
            const string pre = @"<root><foo>bar</foo></root>";
            const string post = @"<root>bar</root>";
            const string xpath = "//foo";
            var task = new ConvertToText();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertCDataSectionToText()
        {
            const string pre = @"<root><![CDATA[text]]></root>";
            const string post = @"<root>text</root>";
            const string xpath = "//text()";
            var task = new ConvertToText();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertCDataSectionWithSpecialCharactersToText()
        {
            const string pre = @"<root><![CDATA[<text>]]></root>";
            const string post = @"<root>&lt;text&gt;</root>";
            const string xpath = "//text()";
            var task = new ConvertToText();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertCommentToText()
        {
            const string pre = @"<root><!--text--></root>";
            const string post = @"<root>text</root>";
            const string xpath = "//comment()";
            var task = new ConvertToText();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertCommentWithSpecialCharactersToText()
        {
            const string pre = @"<root><!--<text>--></root>";
            const string post = @"<root>&lt;text&gt;</root>";
            const string xpath = "//comment()";
            var task = new ConvertToText();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertProcessingInstructionToText()
        {
            const string pre = @"<root><?pi text?></root>";
            const string post = @"<root>text</root>";
            const string xpath = "//processing-instruction()";
            var task = new ConvertToText();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertProcessingInstructionWithSpecialCharactersToText()
        {
            const string pre = @"<root><?pi <text>?></root>";
            const string post = @"<root>&lt;text&gt;</root>";
            const string xpath = "//processing-instruction()";
            var task = new ConvertToText();
            Run(pre, post, xpath, task);
        }
    }
}