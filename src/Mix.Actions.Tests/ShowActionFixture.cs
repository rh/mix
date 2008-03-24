using System;
using System.IO;
using Mix.Core;
using Mix.Core.Exceptions;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class ShowActionFixture
    {
        [Test]
        public void Count()
        {
            using (TextWriter writer = new StringWriter())
            {
                Context context = new Context("<root />", "root", writer);
                context.FileName = "file";
                ShowAction action = new ShowAction();
                action.Execute(context);
                Assert.That(writer.ToString(), Is.EqualTo(String.Format("file: 1{0}<root />{0}", Environment.NewLine)));
            }
        }

        [Test]
        public void Count0()
        {
            using (TextWriter writer = new StringWriter())
            {
                Context context = new Context("<root />", "foo", writer);
                context.FileName = "file";
                ShowAction action = new ShowAction();
                action.Execute(context);
                Assert.That(writer.ToString(), Is.EqualTo(String.Format("file: 0{0}", Environment.NewLine)));
            }
        }

        [Test]
        public void NoSelection()
        {
            using (TextWriter writer = new StringWriter())
            {
                Context context = new Context();
                context.Xml = "<root/>";
                context.FileName = "file";
                context.Output = writer;
                ShowAction action = new ShowAction();
                action.Execute(context);
                Assert.That(writer.ToString(), Is.EqualTo(String.Format("file: no selection.{0}", Environment.NewLine)));
            }
        }

        [Test]
        [ExpectedException(typeof(ActionExecutionException))]
        public void AnInvalidXPathExpressionShouldThrow()
        {
            using (TextWriter writer = new StringWriter())
            {
                Context context = new Context("<root/>", "///");
                context.FileName = "file";
                context.Output = writer;
                ShowAction action = new ShowAction();
                action.Execute(context);
            }
        }
    }
}