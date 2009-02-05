using System;
using System.IO;
using Mix.Core;
using Mix.Core.Exceptions;
using NUnit.Framework;

namespace Mix.Tasks.Tests
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
            var context = new Context(xml, xpath);
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
            var context = new Context(xml, xpath);
            TextWriter writer = new StringWriter();
            context.Output = writer;
            context.FileName = "file";
            new Count().Execute(context);
            var expected = String.Empty;
            Assert.AreEqual(expected, writer.ToString());
        }

        [Test]
        [ExpectedException(typeof(TaskExecutionException))]
        public void InvalidXPath()
        {
            const string pre = @"<root><child/><child/></root>";
            const string post = pre;
            const string xpath = "//";
            var task = new Count();
            Run(pre, post, xpath, task);
        }
    }
}