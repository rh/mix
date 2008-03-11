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
            private int bar;
            private int percentage;

            public int Bar
            {
                get { return bar; }
                set { bar = value; }
            }

            [Range(0, 100)]
            public int Percentage
            {
                get { return percentage; }
                set { percentage = value; }
            }
        }

        private void AssertIsValid(PropertyInfo property, int value)
        {
            string description;
            bool valid = validator.Validate(property, value, out description);
            Assert.IsTrue(valid);
        }

        private void AssertIsInvalid(PropertyInfo property, int value)
        {
            string description;
            bool valid = validator.Validate(property, value, out description);
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
            PropertyInfo property = typeof(Foo).GetProperty("Percentage");
            foreach (int value in new int[] {0, 1, 100})
            {
                AssertIsValid(property, value);
            }
            foreach (int value in new int[] {-1, 101, 200})
            {
                AssertIsInvalid(property, value);
            }
        }

        [Test]
        public void Test2()
        {
            PropertyInfo property = typeof(Foo).GetProperty("Bar");
            foreach (int value in new int[] {1, 2, 100, 100, 10000})
            {
                AssertIsValid(property, value);
            }
            foreach (int value in new int[] {-1, 0})
            {
                AssertIsInvalid(property, value);
            }
        }
    }
}