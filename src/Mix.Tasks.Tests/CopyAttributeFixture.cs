using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class CopyAttributeFixture : TestFixture
    {
        [Test]
        public void CopyToNewAttribute()
        {
            const string pre = @"<root attribute=""value"" />";
            const string post = @"<root attribute=""value"" newattribute=""value"" />";
            const string xpath = "//@attribute";
            var task = new CopyAttribute {Name = "newattribute"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CopyToExistingAttribute()
        {
            const string pre = @"<root attribute=""foo"" existingattribute=""bar""/>";
            const string post = @"<root attribute=""foo"" existingattribute=""foo"" />";
            const string xpath = "//@attribute";
            var task = new CopyAttribute {Name = "existingattribute"};
            Run(pre, post, xpath, task);
        }
    }
}