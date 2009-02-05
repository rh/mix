using NUnit.Framework;
using DescriptionAttribute=Mix.Core.Attributes.DescriptionAttribute;

namespace Mix.Core.Tests
{
    [TestFixture]
    public class DescriptionAttributeFixture
    {
        private class WithoutDescription
        {
        }

        [Description("description")]
        private class WithDescription
        {
        }

        [Test]
        public void ClassWithoutDescription()
        {
            const string defaultValue = "[no description]";
            var description = DescriptionAttribute.GetDescriptionFrom(new WithoutDescription(), defaultValue);
            Assert.AreEqual(defaultValue, description);
        }

        [Test]
        public void ClassWithDescription()
        {
            const string defaultValue = "[no description]";
            var description = DescriptionAttribute.GetDescriptionFrom(new WithDescription(), defaultValue);
            Assert.AreEqual("description", description);
        }
    }
}