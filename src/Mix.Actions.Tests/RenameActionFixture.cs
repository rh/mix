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
            RenameAction action = new RenameAction();
            action.Name = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RenameDocumentElement()
        {
            string pre = @"<root />";
            string post = @"<root />";
            string xpath = "root";
            RenameAction action = new RenameAction();
            action.Name = "root";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RenameElements()
        {
            string pre = @"<root><pre /><pre /></root>";
            string post = @"<root><post /><post /></root>";
            string xpath = "root/pre";
            RenameAction action = new RenameAction();
            action.Name = "post";
            Run(pre, post, xpath, action);
        }

        /*[Test]
        [Ignore("Renaming elements like <rename><rename/></rename> " +
                "is currently not possible; this will result in <renamed><rename/></renamed>")]*/

        public void RenameElementsWithChildElements()
        {
            string pre = @"<root><pre><pre /></pre></root>";
            string post = @"<root><post><post /></post></root>";
            string xpath = "//pre";
            RenameAction action = new RenameAction();
            action.Name = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RenameAttributes()
        {
            string pre = @"<root pre="""" />";
            string post = @"<root post="""" />";
            string xpath = "//@pre";
            RenameAction action = new RenameAction();
            action.Name = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RenameAttributesWithExistingAttribute()
        {
            string pre = @"<root pre="""" />";
            string post = @"<root pre="""" />";
            string xpath = "//@pre";
            RenameAction action = new RenameAction();
            action.Name = "pre";
            Run(pre, post, xpath, action);
        }
    }
}