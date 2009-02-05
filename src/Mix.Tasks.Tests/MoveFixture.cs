using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class MoveFixture : TestFixture
    {
        [Test]
        public void Test()
        {
            const string pre = @"<root><a></a><b></b></root>";
            const string post = @"<root><a><b></b></a></root>";
            const string xpath = "root/b";
            var action = new Move {To = "../a"};
            Run(pre, post, xpath, action);
        }
    }
}