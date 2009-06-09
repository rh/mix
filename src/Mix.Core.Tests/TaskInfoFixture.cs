using Mix.Core.Attributes;
using NUnit.Framework;

namespace Mix.Core.Tests
{
    [TestFixture]
    public class TaskInfoFixture
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
        public void Name()
        {
            var info1 = TaskInfo.For(someTask);
            Assert.AreEqual("name", info1.Name);

            var info2 = TaskInfo.For(otherTask);
            Assert.AreEqual("othername", info2.Name);
        }

        [Test]
        public void Description()
        {
            var info1 = TaskInfo.For(someTask);
            Assert.AreEqual("description", info1.Description);

            var info2 = TaskInfo.For(otherTask);
            Assert.AreEqual("otherdescription", info2.Description);
        }

        [Test]
        public void Options()
        {
            var info1 = TaskInfo.For(someTask);
            Assert.AreEqual(0, info1.Options.Length);

            var info2 = TaskInfo.For(otherTask);
            Assert.AreEqual(1, info2.Options.Length);
        }

        [Attributes.Description("description")]
        private class SomeTask
        {
            public override string ToString()
            {
                return "name";
            }
        }

        [Attributes.Description("otherdescription")]
        private class SomeOtherTask
        {
            [Option, Required]
            public string Name
            {
                get { return ""; }
            }

            public override string ToString()
            {
                return "othername";
            }
        }
    }
}