using System;
using System.IO;
using Mix.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class OutlineActionFixture
    {
        [Test]
        public void Test()
        {
            using (TextWriter writer = new StringWriter())
            {
                Context context = new Context("<root><child><foo/></child></root>", "root", writer);
                context.FileName = "file";
                OutlineAction action = new OutlineAction();
                action.Depth = 1;
                action.Execute(context);
                Assert.That(writer.ToString(), Is.EqualTo(String.Format("file: 1{0}<root>{0}  <child />{0}</root>{0}", Environment.NewLine)));
            }
        }

        [Test]
        public void Test2()
        {
            using (TextWriter writer = new StringWriter())
            {
                Context context = new Context("<root><child><foo/></child></root>", "root", writer);
                context.FileName = "file";
                OutlineAction action = new OutlineAction();
                action.Depth = 2;
                action.Execute(context);
                Assert.That(writer.ToString(), Is.EqualTo(String.Format("file: 1{0}<root>{0}  <child>{0}    <foo />{0}  </child>{0}</root>{0}", Environment.NewLine)));
            }
        }
    }
}