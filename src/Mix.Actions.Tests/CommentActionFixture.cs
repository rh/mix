using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class CommentActionFixture : TestFixture
    {
        [Test]
        public void CommentElement()
        {
            string pre = @"<root><element /></root>";
            string post = @"<root><!--<element />--></root>";
            string xpath = "//element";
            CommentAction action = new CommentAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void CommentElements()
        {
            string pre = @"<root><element /><element /></root>";
            string post = @"<root><!--<element />--><!--<element />--></root>";
            string xpath = "//element";
            CommentAction action = new CommentAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void CommentAttribute()
        {
            string pre = @"<root><element a="""" /><element a="""" /></root>";
            string post = @"<root><!--<element a="""" />--><!--<element a="""" />--></root>";
            string xpath = "//@a";
            CommentAction action = new CommentAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void CommentTextNode()
        {
            string pre = @"<root>text</root>";
            string post = @"<root><!--text--></root>";
            string xpath = "//text()";
            CommentAction action = new CommentAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void CommentCDataSection()
        {
            string pre = @"<root><![CDATA[text]]></root>";
            string post = @"<root><!--<![CDATA[text]]>--></root>";
            string xpath = "//text()";
            CommentAction action = new CommentAction();
            Run(pre, post, xpath, action);
        }
    }
}