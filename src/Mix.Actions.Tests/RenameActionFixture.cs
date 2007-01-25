using Mix.Core;
using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class RenameActionFixture : TestFixture
    {
        [Test]
        public void RenameElement()
        {
            string pre = @"<root><pre /></root>";
            string post = @"<root><post /></root>";
            string xpath = "root/pre";
            Action action = new RenameAction("post");
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RenameElements()
        {
            string pre = @"<root><pre /><pre /></root>";
            string post = @"<root><post /><post /></root>";
            string xpath = "root/pre";
            Action action = new RenameAction("post");
            Run(pre, post, xpath, action);
        }

        [Test]
        [Ignore("Renaming elements like <rename><rename/></rename> " +
                "is currently not possible; this will result in <renamed><rename/></renamed>")]
        public void RenameElementsWithChildElements()
        {
            string pre = @"<root><pre><pre /></pre></root>";
            string post = @"<root><post><post /></post></root>";
            string xpath = "//pre";
            Action action = new RenameAction("post");
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RenameAttributes()
        {
            string pre = @"<root pre="""" />";
            string post = @"<root post="""" />";
            string xpath = "//@pre";
            Action action = new RenameAction("post");
            Run(pre, post, xpath, action);
        }
    }
}
