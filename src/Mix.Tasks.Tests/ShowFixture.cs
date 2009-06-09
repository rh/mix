using System;
using System.IO;
using System.Xml;
using Mix.Core;
using Mix.Core.Exceptions;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class ShowFixture
    {
        [Test]
        public void Count()
        {
            using (TextWriter writer = new StringWriter())
            {
                var document = new XmlDocument();
                document.LoadXml("<root />");
                var context = new Context {Document = document, XPath = "root", Output = writer, FileName = "file"};
                var task = new Show();
                task.Execute(context);
                Assert.That(writer.ToString(), Is.EqualTo(String.Format("file: 1{0}<root />{0}", Environment.NewLine)));
            }
        }

        [Test]
        public void Count0()
        {
            using (TextWriter writer = new StringWriter())
            {
                var document = new XmlDocument();
                document.LoadXml("<root />");
                var context = new Context {Document = document, XPath = "foo", Output = writer, FileName = "file"};
                var task = new Show();
                task.Execute(context);
                Assert.IsTrue(writer.ToString().StartsWith("file: 0"));
            }
        }

        [Test]
        public void NoSelection()
        {
            using (TextWriter writer = new StringWriter())
            {
                var document = new XmlDocument();
                document.LoadXml("<root/>");
                var context = new Context {Document = document, XPath = "//foo", FileName = "file", Output = writer};
                var task = new Show();
                task.Execute(context);
                Assert.That(writer.ToString(), Is.EqualTo(String.Format("file: 0{0}", Environment.NewLine)));
            }
        }

        [Test]
        [ExpectedException(typeof(TaskExecutionException))]
        public void AnInvalidXPathExpressionShouldThrow()
        {
            using (TextWriter writer = new StringWriter())
            {
                var document = new XmlDocument();
                document.LoadXml("<root/>");
                var context = new Context {Document = document, XPath = "///", FileName = "file", Output = writer};
                var task = new Show();
                task.Execute(context);
            }
        }
    }
}