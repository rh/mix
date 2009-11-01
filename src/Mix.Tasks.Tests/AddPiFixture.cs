using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class AddPiFixture : TestFixture
    {
        [Test]
        public void AddToElementWithoutValue()
        {
            const string pre = @"<root></root>";
            const string post = @"<root><?name ?></root>";
            const string xpath = "root";
            var task = new AddPi {Name = "name"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void AddToElement()
        {
            const string pre = @"<root></root>";
            const string post = @"<root><?name value?></root>";
            const string xpath = "root";
            var task = new AddPi { Name = "name", Value = "value" };
            Run(pre, post, xpath, task);
        }

        [Test]
        public void NoSelection()
        {
            const string pre = @"<root></root>";
            const string post = pre;
            const string xpath = "foo";
            var task = new AddPi { Name = "name", Value = "value" };
            Run(pre, post, xpath, task);
        }
    }
}