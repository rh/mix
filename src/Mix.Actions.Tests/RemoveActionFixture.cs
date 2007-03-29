using Mix.Core;
using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class RemoveActionFixture : TestFixture
    {
        [Test]
        public void RemoveElements()
        {
            string pre = @"<root><pre /><dummy /></root>";
            string post = @"<root><dummy /></root>";
            string xpath = "root/pre";
            Action action = new RemoveAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RemoveAttribute()
        {
            string pre = @"<root pre="""" />";
            string post = @"<root />";
            string xpath = "//@pre";
            Action action = new RemoveAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void RemoveAttributes()
        {
            string pre = @"<root pre=""""><node pre=""""/></root>";
            string post = @"<root><node /></root>";
            string xpath = "//@pre";
            Action action = new RemoveAction();
            Run(pre, post, xpath, action);
        }
    }
}