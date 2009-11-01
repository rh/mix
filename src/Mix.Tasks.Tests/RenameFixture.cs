using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class RenameFixture : TestFixture
    {
        [Test]
        public void RenameElement()
        {
            const string pre = @"<root><pre /></root>";
            const string post = @"<root><post /></root>";
            const string xpath = "root/pre";
            var task = new Rename {Name = "post"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RenameDocumentElement()
        {
            const string pre = @"<root />";
            const string post = @"<root />";
            const string xpath = "root";
            var task = new Rename {Name = "root"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RenameElements()
        {
            const string pre = @"<root><pre /><pre /></root>";
            const string post = @"<root><post /><post /></root>";
            const string xpath = "root/pre";
            var task = new Rename {Name = "post"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RenameElementsWithChildElements()
        {
            const string pre = @"<root><pre><pre /></pre></root>";
            const string post = @"<root><post><post /></post></root>";
            const string xpath = "//pre";
            var task = new Rename {Name = "post"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RenameAttributes()
        {
            const string pre = @"<root pre="""" />";
            const string post = @"<root post="""" />";
            const string xpath = "//@pre";
            var task = new Rename {Name = "post"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RenameAttributesWithExistingAttribute()
        {
            const string pre = @"<root pre="""" />";
            const string post = @"<root pre="""" />";
            const string xpath = "//@pre";
            var task = new Rename {Name = "pre"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RenameProcessingInstruction()
        {
            const string pre = @"<root><?foo ?></root>";
            const string post = @"<root><?bar ?></root>";
            const string xpath = "//processing-instruction()";
            var task = new Rename {Name = "bar"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RenameElementWithRegularExpression()
        {
            const string pre = @"<root><p-01-foo /></root>";
            const string post = @"<root><bar-01 /></root>";
            const string xpath = "root/*";
            var task = new Rename {Pattern = @"^p-(\d{2})-foo$", Name = "bar-$1"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void RenameElementWithRegularExpression2()
        {
            const string pre = @"<root><p-01-foo /></root>";
            const string post = @"<root><bar /></root>";
            const string xpath = "root/*";
            var task = new Rename {Name = "bar"};
            Run(pre, post, xpath, task);
        }
    }
}