using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class AddRootElementFixture : TestFixture
    {
        [Test]
        public void Test()
        {
            const string pre = @"<oldroot></oldroot>";
            const string post = @"<newroot><oldroot></oldroot></newroot>";
            AddRootElement action = new AddRootElement();
            action.Name = "newroot";
            Run(pre, post, "", action);
        }
    }
}