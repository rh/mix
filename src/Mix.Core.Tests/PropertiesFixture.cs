using System.Collections.Generic;
using NUnit.Framework;

namespace Mix.Core.Tests
{
    [TestFixture]
    public class PropertiesFixture
    {
        [Test]
        public void EmptyConstructor()
        {
            Properties properties = new Properties();
            Assert.IsNotNull(properties);
        }

        [Test]
        public void ConstructorWithNonNullParam()
        {
            IDictionary<string, string> dictionary =
                new Dictionary<string, string>();
            Properties properties = new Properties(dictionary);
            Assert.IsNotNull(properties);
        }

        [Test]
        public void ConstructorWithNullParam()
        {
            Properties properties = new Properties(null);
            Assert.IsNotNull(properties);
        }

        [Test]
        public void KeyValue()
        {
            const string key = "[key]";
            const string value = "[value]";

            IDictionary<string, string> dictionary =
                new Dictionary<string, string>();
            dictionary[key] = value;

            Properties properties = new Properties(dictionary);

            Assert.IsNotNull(properties);
            Assert.IsTrue(properties.ContainsKey(key));
            Assert.AreEqual(value, properties[key]);
        }
    }
}
