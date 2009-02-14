using System;
using System.IO;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;
using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class ExtractFixture : TestFixture
    {
        [Test]
        [ExpectedException(typeof(XmlException))]
        public void XmlNotSet()
        {
            var task = new Extract();
            task.Execute(Context.Null);
            Assert.Fail("An XmlException should have been thrown.");
        }

        [Test]
        [ExpectedException(typeof(RequirementException))]
        public void NameNotSet()
        {
            var task = new Extract();
            const string xml = @"<root><node/><node/></root>";
            const string xpath = "//node";
            var context = new Context(xml, xpath);
            task.Execute(context);
            Assert.Fail("A RequirementException should have been thrown.");
        }

        [Test]
        public void Name()
        {
            var task = new DerivedExtractTask {Name = "file"};

            const string xml = @"<root><node/><node/></root>";
            const string xpath = "//node";

            var context = new Context(xml, xpath);
            task.Execute(context);
            Assert.AreEqual("file.xml", task.Filename);
        }

        [Test]
        public void NameWithXPath()
        {
            var task = new DerivedExtractTask {Name = "xpath:@file"};

            const string xml = @"<root><node file='file'/><node file='file'/></root>";
            const string xpath = "//node";

            var context = new Context(xml, xpath);
            task.Execute(context);
            Assert.AreEqual("file.xml", task.Filename);
        }

        [Test]
        public void ExtractWithOredXpath()
        {
            var writer = new StringWriter();
            var task = new DerivedExtractTask(writer) {Name = "foobar"};

            const string xml = @"<root><foo /><bar /></root>";
            const string xpath = "//foo|//bar";

            var context = new Context(xml, xpath);
            task.Execute(context);

            const string declaration = @"<?xml version=""1.0"" encoding=""utf-16""?>";
            // NOTE: elements are processed in reverse order.
            var expected =
                String.Format(@"{0}{1}<bar />{0}{1}<foo />",
                              declaration, Environment.NewLine);
            Assert.AreEqual(expected, writer.ToString());
        }

        [ProcessingOrder(ProcessingOrder.Reverse)]
        private class DerivedExtractTask : Extract
        {
            public DerivedExtractTask()
            {
            }

            public DerivedExtractTask(TextWriter writer)
                : base(writer)
            {
            }

            private string filename = String.Empty;

            public string Filename
            {
                get { return filename; }
            }

            protected override string GetFileName(XmlElement element)
            {
                filename = base.GetFileName(element);
                return filename;
            }
        }
    }
}