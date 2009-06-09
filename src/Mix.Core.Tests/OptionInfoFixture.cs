using Mix.Core.Attributes;
using NUnit.Framework;

namespace Mix.Core.Tests
{
    [TestFixture]
    public class OptionInfoFixture
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
            var options = OptionInfo.For(someTask);
            Assert.AreEqual(0, options.Length);

            options = OptionInfo.For(otherTask);
            Assert.AreEqual(2, options.Length);
        }

        [Test]
        public void Name()
        {
            var options = OptionInfo.For(otherTask);
            Assert.AreEqual("Name", options[0].Name);
        }

        [Test]
        public void Description()
        {
            var options = OptionInfo.For(otherTask);
            Assert.AreEqual("[no description]", options[0].Description);
            Assert.AreEqual("Description for Name2", options[1].Description);
        }

        [Test]
        public void Required()
        {
            var options = OptionInfo.For(otherTask);
            Assert.IsTrue(options[0].Required);
            Assert.IsFalse(options[1].Required);
        }

        private class SomeTask
        {
        }

        private class SomeOtherTask
        {
            [Option, Required]
            public string Name
            {
                get { return ""; }
            }

            [Option]
            [Attributes.Description("Description for Name2")]
            public string Name2
            {
                get { return ""; }
            }
        }
    }
}