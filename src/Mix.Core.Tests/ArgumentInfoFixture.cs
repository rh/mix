using Mix.Core.Attributes;
using NUnit.Framework;

namespace Mix.Core.Tests
{
    [TestFixture]
    public class ArgumentInfoFixture
    {
        private SomeAction someAaction;
        private SomeOtherAction otherAction;

        [SetUp]
        public void SetUp()
        {
            someAaction = new SomeAction();
            otherAction = new SomeOtherAction();
        }

        [Test]
        public void Length()
        {
            IArgumentInfo[] arguments = ArgumentInfo.For(someAaction);
            Assert.AreEqual(0, arguments.Length);

            arguments = ArgumentInfo.For(otherAction);
            Assert.AreEqual(2, arguments.Length);
        }

        [Test]
        public void Name()
        {
            IArgumentInfo[] arguments = ArgumentInfo.For(otherAction);
            Assert.AreEqual("Name", arguments[0].Name);
        }

        [Test]
        public void Description()
        {
            IArgumentInfo[] arguments = ArgumentInfo.For(otherAction);
            Assert.AreEqual("[no description]", arguments[0].Description);
            Assert.AreEqual("Description for Name2", arguments[1].Description);
        }

        [Test]
        public void Required()
        {
            IArgumentInfo[] arguments = ArgumentInfo.For(otherAction);
            Assert.IsTrue(arguments[0].Required);
            Assert.IsFalse(arguments[1].Required);
        }

        private class SomeAction
        {
        }

        private class SomeOtherAction
        {
            [Argument, Required]
            public string Name
            {
                get { return ""; }
            }

            [Argument]
            [Attributes.Description("Description for Name2")]
            public string Name2
            {
                get { return ""; }
            }
        }
    }
}