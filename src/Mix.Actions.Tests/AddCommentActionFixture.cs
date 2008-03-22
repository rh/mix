using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class AddCommentActionFixture : TestFixture
    {
        [Test]
        public void AddToElement()
        {
            const string pre = @"<root></root>";
            const string post = @"<root><!--COMMENT--></root>";
            const string xpath = "root";
            AddCommentAction action = new AddCommentAction();
            action.Value = "COMMENT";
            Run(pre, post, xpath, action);
        }
    }
}