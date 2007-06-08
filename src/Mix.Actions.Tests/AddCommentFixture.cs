using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class AddCommentFixture : TestFixture
    {
        [Test]
        public void AddToElement()
        {
            const string pre = @"<root></root>";
            const string post = @"<root><!--COMMENT--></root>";
            const string xpath = "root";
            AddCommentAction action = new AddCommentAction();
            action.Text = "COMMENT";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AddToAttribute()
        {
            const string pre = @"<root a=""""></root>";
            const string post = @"<root a=""""><!--COMMENT--></root>";
            const string xpath = "root/@a";
            AddCommentAction action = new AddCommentAction();
            action.Text = "COMMENT";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AddToTextNode()
        {
            const string pre = @"<root>text</root>";
            const string post = @"<root>text<!--COMMENT--></root>";
            const string xpath = "//text()";
            AddCommentAction action = new AddCommentAction();
            action.Text = "COMMENT";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AddToComment()
        {
            const string pre = @"<root><!--COMMENT--></root>";
            const string post = @"<root><!--COMMENTCOMMENT--></root>";
            const string xpath = "//comment()";
            AddCommentAction action = new AddCommentAction();
            action.Text = "COMMENT";
            Run(pre, post, xpath, action);
        }
    }
}