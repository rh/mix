using System;
using System.IO;
using System.Xml;
using Mix.Tasks;
using NUnit.Framework;

namespace Mix.Tests
{
    [TestFixture]
    public class OutlineFixture
    {
        [Test]
        public void Test()
        {
            using (TextWriter writer = new StringWriter())
            {
                var document = new XmlDocument();
                document.LoadXml("<root><child><foo/></child></root>");
                var context = new Context {Document = document, XPath = "root", Output = writer, Quiet = writer, FileName = "file"};
                var task = new Outline {Depth = 1};
                task.Execute(context);
                Assert.That(writer.ToString(), Is.EqualTo(string.Format("file: 1{0}<root>{0}  <child />{0}</root>{0}{0}", Environment.NewLine)));
            }
        }

        [Test]
        public void Test2()
        {
            using (TextWriter writer = new StringWriter())
            {
                var document = new XmlDocument();
                document.LoadXml("<root><child><foo/></child></root>");
                var context = new Context {Document = document, XPath = "root", Output = writer, Quiet = writer, FileName = "file"};
                var task = new Outline {Depth = 2};
                task.Execute(context);
                Assert.That(writer.ToString(), Is.EqualTo(string.Format("file: 1{0}<root>{0}  <child>{0}    <foo />{0}  </child>{0}</root>{0}{0}", Environment.NewLine)));
            }
        }
    }
}
