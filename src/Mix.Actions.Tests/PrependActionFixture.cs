using Mix.Core;
using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class PrependActionFixture : TestFixture
    {
        [Test]
        public void PrependElements()
        {
            string pre = @"<root>pre</root>";
            string post = @"<root>prefixpre</root>";
            string xpath = "root";
            Action action = new PrependAction("prefix");
            Run(pre, post, xpath, action);
        }

        [Test]
        public void PrependAttributes()
        {
            string pre = @"<root pre=""pre"" />";
            string post = @"<root pre=""prefixpre"" />";
            string xpath = "root/@pre";
            Action action = new PrependAction("prefix");
            Run(pre, post, xpath, action);
        }
    }
}
