using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class AddRootElementActionFixture : TestFixture
    {
        [Test]
        public void Test()
        {
            const string pre = @"<oldroot></oldroot>";
            const string post = @"<newroot><oldroot></oldroot></newroot>";
            AddRootElementAction action = new AddRootElementAction();
            action.Name = "newroot";
            Run(pre, post, "", action);
        }
    }
}