using System;
using System.IO;
using System.Xml;
using Mix.Core;
using Mix.Core.Exceptions;
using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class ExtractActionFixture : TestFixture
    {
        [Test]
        public void EmptyConstructor()
        {
            ExtractAction action = new ExtractAction();
            Assert.IsNotNull(action.Name);
            Assert.AreEqual(0, action.Name.Length);
        }

        [Test]
        [ExpectedException(typeof(XmlException))]
        public void XmlNotSet()
        {
            ExtractAction action = new ExtractAction();
            action.Execute(Context.Null);
            Assert.Fail("An XmlException should have been thrown.");
        }

        [Test]
        [ExpectedException(typeof(RequirementException))]
        public void NameNotSet()
        {
            ExtractAction action = new ExtractAction();
            string xml = @"<root><node/><node/></root>";
            string xpath = "//node";
            Context context = new Context(xml, xpath);
            action.Execute(context);
            Assert.Fail("A RequirementException should have been thrown.");
        }

        [Test]
        public void Name()
        {
            DerivedExtractAction action = new DerivedExtractAction();
            action.Name = "file";

            string xml = @"<root><node/><node/></root>";
            string xpath = "//node";

            Context context = new Context(xml, xpath);
            action.Execute(context);
            Assert.AreEqual("file.xml", action.Filename);
        }

        [Test]
        public void NameWithXPath()
        {
            DerivedExtractAction action = new DerivedExtractAction();
            action.Name = "xpath:@file";

            string xml = @"<root><node file='file'/><node file='file'/></root>";
            string xpath = "//node";

            Context context = new Context(xml, xpath);
            action.Execute(context);
            Assert.AreEqual("file.xml", action.Filename);
        }

        [Test]
        public void ExtractWithOredXpath()
        {
            StringWriter writer = new StringWriter();
            DerivedExtractAction action = new DerivedExtractAction(writer);
            action.Name = "foobar";

            string xml = @"<root><foo /><bar /></root>";
            string xpath = "//foo|//bar";

            Context context = new Context(xml, xpath);
            action.Execute(context);

            string declaration = @"<?xml version=""1.0"" encoding=""utf-16""?>";
            // NOTE: elements are processed in reverse order.
            string expected =
                String.Format(@"{0}{1}<bar />{0}{1}<foo />",
                              declaration, Environment.NewLine);
            Assert.AreEqual(expected, writer.ToString());
        }

        private class DerivedExtractAction : ExtractAction
        {
            public DerivedExtractAction()
            {
            }

            public DerivedExtractAction(TextWriter writer)
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