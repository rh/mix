using System;
using System.IO;
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
                var context = new Context("<root />", "root", writer) {FileName = "file"};
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
                var context = new Context("<root />", "foo", writer) {FileName = "file"};
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
                var context = new Context("<root/>", "//foo") {FileName = "file", Output = writer};
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
                var context = new Context("<root/>", "///") {FileName = "file", Output = writer};
                var task = new Show();
                task.Execute(context);
            }
        }
    }
}