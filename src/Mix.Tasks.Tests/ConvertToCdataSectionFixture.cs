using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class ConvertToCdataSectionFixture : TestFixture
    {
        [Test]
        public void ConvertElementCdataSection()
        {
            const string pre = @"<root><foo>bar</foo></root>";
            const string post = @"<root><![CDATA[<foo>bar</foo>]]></root>";
            const string xpath = "//foo";
            var task = new ConvertToCdataSection();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertTextToCdataSection()
        {
            const string pre = @"<root>text</root>";
            const string post = @"<root><![CDATA[text]]></root>";
            const string xpath = "//text()";
            var task = new ConvertToCdataSection();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertCommentCdataSection()
        {
            const string pre = @"<root><!--text--></root>";
            const string post = @"<root><![CDATA[text]]></root>";
            const string xpath = "//comment()";
            var task = new ConvertToCdataSection();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertProcessingInstructionCdataSection()
        {
            const string pre = @"<root><?pi text?></root>";
            const string post = @"<root><![CDATA[text]]></root>";
            const string xpath = "//processing-instruction()";
            var task = new ConvertToCdataSection();
            Run(pre, post, xpath, task);
        }
    }
}