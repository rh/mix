using Mix.Core.Attributes;
using NUnit.Framework;

namespace Mix.Core.Tests
{
    [TestFixture]
    public class ActionInfoFixture
    {
        private SomeAction someAction;
        private SomeOtherAction otherAction;

        [SetUp]
        public void SetUp()
        {
            someAction = new SomeAction();
            otherAction = new SomeOtherAction();
        }

        [Test]
        public void Name()
        {
            IActionInfo info1 = ActionInfo.For(someAction);
            Assert.AreEqual("name", info1.Name);

            IActionInfo info2 = ActionInfo.For(otherAction);
            Assert.AreEqual("othername", info2.Name);
        }

        [Test]
        public void Description()
        {
            IActionInfo info1 = ActionInfo.For(someAction);
            Assert.AreEqual("description", info1.Description);

            IActionInfo info2 = ActionInfo.For(otherAction);
            Assert.AreEqual("otherdescription", info2.Description);
        }

        [Test]
        public void Arguments()
        {
            IActionInfo info1 = ActionInfo.For(someAction);
            Assert.AreEqual(0, info1.Arguments.Length);

            IActionInfo info2 = ActionInfo.For(otherAction);
            Assert.AreEqual(1, info2.Arguments.Length);
        }

        [Attributes.Description("description")]
        private class SomeAction
        {
            public override string ToString()
            {
                return "name";
            }
        }

        [Attributes.Description("otherdescription")]
        private class SomeOtherAction
        {
            [Argument, Required]
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