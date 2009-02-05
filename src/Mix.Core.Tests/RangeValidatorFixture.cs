using System.Reflection;
using Mix.Core.Attributes;
using NUnit.Framework;

namespace Mix.Core.Tests
{
    [TestFixture]
    public class RangeValidatorFixture
    {
        private RangeValidator validator;

        private class Foo
        {
            public int Bar { get; set; }

            [Range(0, 100)]
            public int Percentage { get; set; }
        }

        private void AssertIsValid(PropertyInfo property, int value)
        {
            string description;
            var valid = validator.Validate(property, value, out description);
            Assert.IsTrue(valid);
        }

        private void AssertIsInvalid(PropertyInfo property, int value)
        {
            string description;
            var valid = validator.Validate(property, value, out description);
            Assert.IsFalse(valid);
        }

        [SetUp]
        public void SetUp()
        {
            validator = new RangeValidator();
        }

        [Test]
        public void Test()
        {
            var property = typeof(Foo).GetProperty("Percentage");
            foreach (var value in new[] {0, 1, 100})
            {
                AssertIsValid(property, value);
            }
            foreach (var value in new[] {-1, 101, 200})
            {
                AssertIsInvalid(property, value);
            }
        }

        [Test]
        public void Test2()
        {
            var property = typeof(Foo).GetProperty("Bar");
            foreach (var value in new[] {1, 2, 100, 100, 10000})
            {
                AssertIsValid(property, value);
            }
            foreach (var value in new[] {-1, 0})
            {
                AssertIsInvalid(property, value);
            }
        }
    }
}