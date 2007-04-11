using Mix.Core.Attributes;
using NUnit.Framework;

namespace Mix.Core.Tests
{
    [TestFixture]
    public class DescriptionAttributeFixture
    {
        #region Private test classes

        private class WithoutDescription
        {
        }

        [Description("description")]
        private class WithDescription
        {
        }

        #endregion

        [Test]
        public void ClassWithoutDescription()
        {
            string defaultValue = "[no description]";
            string description =
                DescriptionAttribute.GetDescriptionFrom(new WithoutDescription(),
                                                        defaultValue);
            Assert.AreEqual(defaultValue, description);
        }

        [Test]
        public void ClassWithDescription()
        {
            string defaultValue = "[no description]";
            string description =
                DescriptionAttribute.GetDescriptionFrom(new WithDescription(),
                                                        defaultValue);
            Assert.AreEqual("description", description);
        }
    }
}