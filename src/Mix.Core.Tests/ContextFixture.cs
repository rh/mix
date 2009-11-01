using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Mix.Core.Tests
{
    [TestFixture]
    public class ContextFixture
    {
        [Test]
        public void EmptyConstructor()
        {
            IContext context = new Context();
            Assert.IsNotNull(context.XPath);
            Assert.IsNotNull(context.Output);
            Assert.IsNotNull(context.Error);
        }

        [Test]
        public void Constructor5()
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            IContext context = new Context(dictionary);
            Assert.IsNotNull(context.XPath);
            Assert.IsNotNull(context.Output);
            Assert.IsNotNull(context.Error);
        }

        [Test]
        public void NullContext()
        {
            var context = Context.Null;
            Assert.IsNotNull(context.XPath);
            Assert.IsNotNull(context.Output);
            Assert.IsNotNull(context.Error);
            Assert.AreEqual(context, Context.Null);
            Assert.AreEqual(0, context.GetHashCode());
        }

        [Test]
        public void Output()
        {
            var context = Context.Null as Context;
            Assert.IsNotNull(context);
            var writer = new StringWriter();
            context.Output = writer;
            context.Error = writer;
            context.Output.Write("test");
            context.Error.Write("test");
            Assert.AreEqual("testtest", writer.ToString());
        }

        [Test]
        public void FormatFileName()
        {
            var context = new Context {FileName = @".\file.xml"};
            Assert.AreEqual("file.xml", context.FileName);
        }
    }
}