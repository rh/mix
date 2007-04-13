using System;
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
        [ExpectedException(typeof(RequirementException))]
        public void NameNotSet()
        {
            ExtractAction action = new ExtractAction();
            action.Execute(Context.Null);
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

        private class DerivedExtractAction : ExtractAction
        {
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