using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class ClearActionFixture : TestFixture
    {
        [Test]
        public void ClearElements()
        {
            string pre = @"<root><child>something</child></root>";
            string post = @"<root></root>";
            string xpath = "root";
            ClearAction action = new ClearAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ClearAttribute()
        {
            string pre = @"<root pre=""something"" />";
            string post = @"<root pre="""" />";
            string xpath = "//@pre";
            ClearAction action = new ClearAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ClearAttributes()
        {
            string pre = @"<root pre=""something""><node pre=""something""/></root>";
            string post = @"<root pre=""""><node pre="""" /></root>";
            string xpath = "//@pre";
            ClearAction action = new ClearAction();
            Run(pre, post, xpath, action);
        }
    }
}