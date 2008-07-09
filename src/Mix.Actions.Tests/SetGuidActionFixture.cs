using System;
using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class SetGuidActionFixture : TestFixture
    {
        private const string Guid = "[not-a-guid-but-who-cares]";

        [Test]
        public void ElementWithOneTextNode()
        {
            string pre = @"<root>pre</root>";
            string post = @"<root>" + Guid + "</root>";
            string xpath = "root";
            DerivedSetGuidAction action = new DerivedSetGuidAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void NormalText()
        {
            string pre = @"<root></root>";
            string post = @"<root>" + Guid + "</root>";
            string xpath = "root";
            DerivedSetGuidAction action = new DerivedSetGuidAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ElementWithMixedChildNodes()
        {
            string pre = @"<root>pre<![CDATA[pre]]>pre</root>";
            string post = @"<root>" + Guid + "</root>";
            string xpath = "root";
            DerivedSetGuidAction action = new DerivedSetGuidAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ElementWithoutChildNodes()
        {
            string pre = @"<root></root>";
            string post = @"<root>" + Guid + "</root>";
            string xpath = "root";
            DerivedSetGuidAction action = new DerivedSetGuidAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Attribute()
        {
            string pre = @"<root attribute=""""></root>";
            string post = String.Format("<root attribute=\"{0}\"></root>", Guid);
            string xpath = "root/@attribute";
            DerivedSetGuidAction action = new DerivedSetGuidAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Attributes()
        {
            string pre = @"<root a="""" b="""" c=""""></root>";
            string post = String.Format("<root a=\"{0}\" b=\"{0}\" c=\"{0}\"></root>", Guid);
            string xpath = "//@*";
            DerivedSetGuidAction action = new DerivedSetGuidAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void TextNodes()
        {
            string pre = @"<root>text</root>";
            string post = @"<root>" + Guid + "</root>";
            string xpath = "//text()";
            DerivedSetGuidAction action = new DerivedSetGuidAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void CDataSections()
        {
            string pre = @"<root><![CDATA[text]]></root>";
            string post = @"<root><![CDATA[" + Guid + "]]></root>";
            string xpath = "//text()";
            DerivedSetGuidAction action = new DerivedSetGuidAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Comments()
        {
            string pre = @"<root><!--COMMENT--><!--COMMENT--></root>";
            string post = @"<root><!--" + Guid + "--><!--" + Guid + "--></root>";
            string xpath = "//comment()";
            DerivedSetGuidAction action = new DerivedSetGuidAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ProcessingInstructions()
        {
            string pre = @"<root><?foo bar?><?foo bar?></root>";
            string post = @"<root><?foo " + Guid + "?><?foo " + Guid + "?></root>";
            string xpath = "//processing-instruction()";
            DerivedSetGuidAction action = new DerivedSetGuidAction();
            Run(pre, post, xpath, action);
        }

        private class DerivedSetGuidAction : SetGuidAction
        {
            protected override string NewGuid()
            {
                return Guid;
            }
        }
    }
}