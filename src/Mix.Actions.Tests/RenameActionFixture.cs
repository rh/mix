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
            Rename action = new Rename();
            action.Name = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RenameDocumentElement()
        {
            string pre = @"<root />";
            string post = @"<root />";
            string xpath = "root";
            Rename action = new Rename();
            action.Name = "root";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RenameElements()
        {
            string pre = @"<root><pre /><pre /></root>";
            string post = @"<root><post /><post /></root>";
            string xpath = "root/pre";
            Rename action = new Rename();
            action.Name = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RenameElementsWithChildElements()
        {
            string pre = @"<root><pre><pre /></pre></root>";
            string post = @"<root><post><post /></post></root>";
            string xpath = "//pre";
            Rename action = new Rename();
            action.Name = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RenameAttributes()
        {
            string pre = @"<root pre="""" />";
            string post = @"<root post="""" />";
            string xpath = "//@pre";
            Rename action = new Rename();
            action.Name = "post";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RenameAttributesWithExistingAttribute()
        {
            string pre = @"<root pre="""" />";
            string post = @"<root pre="""" />";
            string xpath = "//@pre";
            Rename action = new Rename();
            action.Name = "pre";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RenameProcessingInstruction()
        {
            string pre = @"<root><?foo ?></root>";
            string post = @"<root><?bar ?></root>";
            string xpath = "//processing-instruction()";
            Rename action = new Rename();
            action.Name = "bar";
            Run(pre, post, xpath, action);
        }
    }
}