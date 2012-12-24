using System;
using System.IO;
using System.Xml;
using Mix.Exceptions;
using Mix.Tasks;
using NUnit.Framework;

namespace Mix.Tests
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
                var context = new Context {Document = document, XPath = "root", Output = writer, Quiet = writer, FileName = "file"};
                var task = new Show();
                task.Execute(context);
                Assert.That(writer.ToString(), Is.EqualTo(string.Format("file: 1{0}<root />{0}", Environment.NewLine)));
            }
        }

        [Test]
        public void Count0()
        {
            using (TextWriter writer = new StringWriter())
            {
                var document = new XmlDocument();
                document.LoadXml("<root />");
                var context = new Context {Document = document, XPath = "foo", Output = writer, Quiet = writer, FileName = "file"};
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
                var context = new Context {Document = document, XPath = "//foo", FileName = "file", Output = writer, Quiet = writer};
                var task = new Show();
                task.Execute(context);
                Assert.That(writer.ToString(), Is.EqualTo(string.Format("file: 0{0}", Environment.NewLine)));
            }
        }

        [Test]
        public void AnInvalidXPathExpressionShouldThrow()
        {
            using (TextWriter writer = new StringWriter())
            {
                var document = new XmlDocument();
                document.LoadXml("<root/>");
                var context = new Context {Document = document, XPath = "///", FileName = "file", Output = writer, Quiet = writer};
                var task = new Show();

                Assert.Throws<TaskExecutionException>(() => task.Execute(context));
            }
        }
    }
}
