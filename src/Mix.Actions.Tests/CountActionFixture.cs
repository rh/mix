using System;
using System.IO;
using Mix.Core;
using Mix.Core.Exceptions;
using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class CountActionFixture : TestFixture
    {
        [Test]
        public void Test()
        {
            string pre = @"<root><child/><child/></root>";
            string post = pre;
            string xpath = "//child";
            Action action = new CountAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Output()
        {
            string xml = @"<root><child/><child/></root>";
            string xpath = "//child";
            Context context = new Context(xml, xpath);
            TextWriter writer = new StringWriter();
            context.Output = writer;
            context.FileName = "file";
            new CountAction().Execute(context);
            string expected = String.Format("file: 2{0}", Environment.NewLine);
            Assert.AreEqual(expected, writer.ToString());
        }

        [Test]
        public void NoSelection()
        {
            string xml = @"<root><child/><child/></root>";
            string xpath = null;
            Context context = new Context(xml, xpath);
            TextWriter writer = new StringWriter();
            context.Output = writer;
            context.FileName = "file";
            new CountAction().Execute(context);
            string expected = String.Format("file: no selection.{0}", Environment.NewLine);
            Assert.AreEqual(expected, writer.ToString());
        }

        [Test]
        [ExpectedException(typeof(ActionExecutionException))]
        public void InvalidXPath()
        {
            string pre = @"<root><child/><child/></root>";
            string post = pre;
            string xpath = "//";
            Action action = new CountAction();
            Run(pre, post, xpath, action);
        }
    }
}