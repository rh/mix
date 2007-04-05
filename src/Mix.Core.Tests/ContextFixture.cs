using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace Mix.Core.Tests
{
    [TestFixture]
    public class ContextFixture
    {
        private const string xml =
            @"<?xml version=""1.0"" encoding=""UTF-8""?><root/>";
        private const string xpath = "/root";

        [Test]
        public void EmptyConstructor()
        {
            IContext context = new Context();
            Assert.IsNotNull(context.Xml);
            Assert.IsNotNull(context.XPath);
            Assert.IsNotNull(context.Output);
            Assert.IsNotNull(context.Error);
        }

        [Test]
        public void Constructor1()
        {
            IContext context = new Context(xml);
            Assert.IsNotNull(context.Xml);
            Assert.IsNotNull(context.XPath);
            Assert.IsNotNull(context.Output);
            Assert.IsNotNull(context.Error);
        }

        [Test]
        public void Constructor2()
        {
            IContext context = new Context(xml, xpath);
            Assert.IsNotNull(context.Xml);
            Assert.IsNotNull(context.XPath);
            Assert.IsNotNull(context.Output);
            Assert.IsNotNull(context.Error);
        }

        [Test]
        public void Constructor3()
        {
            TextWriter writer = new StringWriter();
            IContext context = new Context(xml, xpath, writer);
            Assert.IsNotNull(context.Xml);
            Assert.IsNotNull(context.XPath);
            Assert.IsNotNull(context.Output);
            Assert.IsNotNull(context.Error);
        }

        [Test]
        public void Constructor4()
        {
            TextWriter writer = new StringWriter();
            IContext context = new Context(xml, xpath, writer, writer);
            Assert.IsNotNull(context.Xml);
            Assert.IsNotNull(context.XPath);
            Assert.IsNotNull(context.Output);
            Assert.IsNotNull(context.Error);
        }

        [Test]
        public void Constructor5()
        {
            IDictionary<string, string> dictionary =
                new Dictionary<string, string>();
            IContext context = new Context(dictionary);
            Assert.IsNotNull(context.Xml);
            Assert.IsNotNull(context.XPath);
            Assert.IsNotNull(context.Output);
            Assert.IsNotNull(context.Error);
        }

        [Test]
        public void NullContext()
        {
            IContext context = Context.Null;
            Assert.IsNotNull(context.Xml);
            Assert.IsNotNull(context.XPath);
            Assert.IsNotNull(context.Output);
            Assert.IsNotNull(context.Error);
        }

        [Test]
        public void Output()
        {
            Context context = Context.Null as Context;
            Assert.IsNotNull(context);
            StringWriter writer = new StringWriter();
            context.Output = writer;
            context.Error = writer;
            context.Output.Write("test");
            context.Error.Write("test");
            Assert.AreEqual("testtest", writer.ToString());
        }
    }
}