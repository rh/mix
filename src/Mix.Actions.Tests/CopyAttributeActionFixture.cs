using Mix.Core;
using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class CopyAttributeActionFixture : TestFixture
    {
        [Test]
        public void CopyToNewAttribute()
        {
            const string pre = @"<root attribute=""value"" />";
            const string post = @"<root attribute=""value"" newattribute=""value"" />";
            const string xpath = "//@attribute";
            Action action = new CopyAttributeAction("newattribute");
            Run(pre, post, xpath, action);
        }

        [Test]
        public void CopyToExistingAttribute()
        {
            const string pre = @"<root attribute=""foo"" existingattribute=""bar""/>";
            const string post = @"<root attribute=""foo"" existingattribute=""foo"" />";
            const string xpath = "//@attribute";
            Action action = new CopyAttributeAction("existingattribute");
            Run(pre, post, xpath, action);
        }
    }
}
