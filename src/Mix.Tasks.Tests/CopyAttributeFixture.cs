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
            CopyAttribute action = new CopyAttribute();
            action.Name = "newattribute";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void CopyToExistingAttribute()
        {
            const string pre = @"<root attribute=""foo"" existingattribute=""bar""/>";
            const string post = @"<root attribute=""foo"" existingattribute=""foo"" />";
            const string xpath = "//@attribute";
            CopyAttribute action = new CopyAttribute();
            action.Name = "existingattribute";
            Run(pre, post, xpath, action);
        }
    }
}