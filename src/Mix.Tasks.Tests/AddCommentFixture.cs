using NUnit.Framework;

namespace Mix.Tasks.Tests
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
            AddComment action = new AddComment();
            action.Value = "COMMENT";
            Run(pre, post, xpath, action);
        }
    }
}