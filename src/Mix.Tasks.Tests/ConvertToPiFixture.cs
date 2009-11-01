using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class ConvertToPiFixture : TestFixture
    {
        [Test]
        public void ConvertElementToProcessingInstruction()
        {
            const string pre = @"<root><foo>bar</foo></root>";
            const string post = @"<root><?foo bar?></root>";
            const string xpath = "//foo";
            var task = new ConvertToPi();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertAttributeToProcessingInstruction()
        {
            const string pre = @"<root name=""value""/>";
            const string post = @"<root><?name value?></root>";
            const string xpath = "/root/@name";
            var task = new ConvertToPi();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertAttributeToProcessingInstructionAndPrepend()
        {
            const string pre = @"<root a=""a"" b=""b"" c=""c""><a /><c /></root>";
            const string post = @"<root a=""a"" c=""c""><?b b?><a /><c /></root>";
            const string xpath = "/root/@b";
            var task = new ConvertToPi();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertTextToProcessingInstruction()
        {
            const string pre = @"<root>text</root>";
            const string post = @"<root><?pi text?></root>";
            const string xpath = "//text()";
            var task = new ConvertToPi {Name = "pi"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertTextWithSpecialCharactersToProcessingInstruction()
        {
            const string pre = @"<root>text&gt;</root>";
            const string post = @"<root><?pi text>?></root>";
            const string xpath = "//text()";
            var task = new ConvertToPi {Name = "pi"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertCdataSectionToProcessingInstruction()
        {
            const string pre = @"<root><![CDATA[text]]></root>";
            const string post = @"<root><?pi text?></root>";
            const string xpath = "//text()";
            var task = new ConvertToPi {Name = "pi"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertCdataSectionWithSpecialCharactersToProcessingInstruction()
        {
            const string pre = @"<root><![CDATA[text>]]></root>";
            const string post = @"<root><?pi text>?></root>";
            const string xpath = "//text()";
            var task = new ConvertToPi {Name = "pi"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertCommentToProcessingInstruction()
        {
            const string pre = @"<root><!--text--></root>";
            const string post = @"<root><?pi text?></root>";
            const string xpath = "//comment()";
            var task = new ConvertToPi {Name = "pi"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ConvertCommentWithSpecialCharactersToProcessingInstruction()
        {
            const string pre = @"<root><!--text>--></root>";
            const string post = @"<root><?pi text>?></root>";
            const string xpath = "//comment()";
            var task = new ConvertToPi {Name = "pi"};
            Run(pre, post, xpath, task);
        }
    }
}