using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class ConvertToCommentFixture : TestFixture
    {
        [Test]
        public void CommentElement()
        {
            const string pre = @"<root><element /></root>";
            const string post = @"<root><!--<element />--></root>";
            const string xpath = "//element";
            var task = new ConvertToComment();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CommentElements()
        {
            const string pre = @"<root><element /><element /></root>";
            const string post = @"<root><!--<element />--><!--<element />--></root>";
            const string xpath = "//element";
            var task = new ConvertToComment();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CommentAttribute()
        {
            const string pre = @"<root><element a="""" /><element a="""" /></root>";
            const string post = pre; // Attributes cannot be commented
            const string xpath = "//@a";
            var task = new ConvertToComment();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CommentTextNode()
        {
            const string pre = @"<root>text</root>";
            const string post = @"<root><!--text--></root>";
            const string xpath = "//text()";
            var task = new ConvertToComment();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CommentCDataSection()
        {
            const string pre = @"<root><![CDATA[text]]></root>";
            const string post = @"<root><!--<![CDATA[text]]>--></root>";
            const string xpath = "//text()";
            var task = new ConvertToComment();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CommentProcessingInstruction()
        {
            const string pre = @"<root><?foo bar ?></root>";
            const string post = @"<root><!--<?foo bar ?>--></root>";
            const string xpath = "//processing-instruction()";
            var task = new ConvertToComment();
            Run(pre, post, xpath, task);
        }
    }
}