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

        [Attributes.Description("description")]
        private class WithDescription
        {
        }

        [Test]
        public void ClassWithoutDescription()
        {
            string defaultValue = "[no description]";
            string description = DescriptionAttribute.GetDescriptionFrom(new WithoutDescription(), defaultValue);
            Assert.AreEqual(defaultValue, description);
        }

        [Test]
        public void ClassWithDescription()
        {
            string defaultValue = "[no description]";
            string description = DescriptionAttribute.GetDescriptionFrom(new WithDescription(), defaultValue);
            Assert.AreEqual("description", description);
        }
    }
}