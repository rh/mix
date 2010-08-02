using System;
using System.IO;
using System.Xml;
using Mix.Exceptions;
using Mix.Tasks;
using NUnit.Framework;

namespace Mix.Tests
{
    [TestFixture]
    public class CountFixture : TestFixture
    {
        [Test]
        public void Test()
        {
            const string pre = @"<root><child /><child /></root>";
            const string post = pre;
            const string xpath = "//child";
            var task = new Count();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void Output()
        {
            const string xml = @"<root><child/><child/></root>";
            const string xpath = "//child";
            var document = new XmlDocument();
            document.LoadXml(xml);
            var context = new Context {Document = document, XPath = xpath};
            TextWriter writer = new StringWriter();
            context.Output = writer;
            context.FileName = "file";
            new Count().Execute(context);
            var expected = String.Format("file: 2{0}", Environment.NewLine);
            Assert.AreEqual(expected, writer.ToString());
        }

        [Test]
        public void NoSelection()
        {
            const string xml = @"<root><child/><child/></root>";
            const string xpath = "//foo";
            var document = new XmlDocument();
            document.LoadXml(xml);
            var context = new Context {Document = document, XPath = xpath};
            TextWriter writer = new StringWriter();
            context.Output = writer;
            context.FileName = "file";
            new Count().Execute(context);
            Assert.AreEqual("file: 0", writer.ToString().Trim());
        }

        [Test]
        public void InvalidXPath()
        {
            const string pre = @"<root><child/><child/></root>";
            const string xpath = "//";
            var task = new Count();

            Assert.Throws<TaskExecutionException>(() => Run(pre, null, xpath, task));
        }
    }
}