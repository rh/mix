using Mix.Core.Attributes;
using NUnit.Framework;

namespace Mix.Core.Tests
{
    [TestFixture]
    public class ArgumentInfoFixture
    {
        private SomeTask someTask;
        private SomeOtherTask otherTask;

        [SetUp]
        public void SetUp()
        {
            someTask = new SomeTask();
            otherTask = new SomeOtherTask();
        }

        [Test]
        public void Length()
        {
            var arguments = ArgumentInfo.For(someTask);
            Assert.AreEqual(0, arguments.Length);

            arguments = ArgumentInfo.For(otherTask);
            Assert.AreEqual(2, arguments.Length);
        }

        [Test]
        public void Name()
        {
            var arguments = ArgumentInfo.For(otherTask);
            Assert.AreEqual("Name", arguments[0].Name);
        }

        [Test]
        public void Description()
        {
            var arguments = ArgumentInfo.For(otherTask);
            Assert.AreEqual("[no description]", arguments[0].Description);
            Assert.AreEqual("Description for Name2", arguments[1].Description);
        }

        [Test]
        public void Required()
        {
            var arguments = ArgumentInfo.For(otherTask);
            Assert.IsTrue(arguments[0].Required);
            Assert.IsFalse(arguments[1].Required);
        }

        private class SomeTask
        {
        }

        private class SomeOtherTask
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