using System;
using Mix.Tasks;
using NUnit.Framework;

namespace Mix.Tests
{
    [TestFixture]
    public class SetGuidFixture : TestFixture
    {
        private const string Guid = "[not-a-guid-but-who-cares]";

        [Test]
        public void ElementWithOneTextNode()
        {
            const string pre = @"<root>pre</root>";
            const string post = @"<root>" + Guid + "</root>";
            const string xpath = "root";
            var task = new DerivedSetGuid();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void NormalText()
        {
            const string pre = @"<root></root>";
            const string post = @"<root>" + Guid + "</root>";
            const string xpath = "root";
            var task = new DerivedSetGuid();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ElementWithMixedChildNodes()
        {
            const string pre = @"<root>pre<![CDATA[pre]]>pre</root>";
            const string post = @"<root>" + Guid + "</root>";
            const string xpath = "root";
            var task = new DerivedSetGuid();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ElementWithoutChildNodes()
        {
            const string pre = @"<root></root>";
            const string post = @"<root>" + Guid + "</root>";
            const string xpath = "root";
            var task = new DerivedSetGuid();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void Attribute()
        {
            const string pre = @"<root attribute=""""></root>";
            var post = String.Format("<root attribute=\"{0}\"></root>", Guid);
            const string xpath = "root/@attribute";
            var task = new DerivedSetGuid();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void Attributes()
        {
            const string pre = @"<root a="""" b="""" c=""""></root>";
            var post = String.Format("<root a=\"{0}\" b=\"{0}\" c=\"{0}\"></root>", Guid);
            const string xpath = "//@*";
            var task = new DerivedSetGuid();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void TextNodes()
        {
            const string pre = @"<root>text</root>";
            const string post = @"<root>" + Guid + "</root>";
            const string xpath = "//text()";
            var task = new DerivedSetGuid();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CDataSections()
        {
            const string pre = @"<root><![CDATA[text]]></root>";
            const string post = @"<root><![CDATA[" + Guid + "]]></root>";
            const string xpath = "//text()";
            var task = new DerivedSetGuid();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void Comments()
        {
            const string pre = @"<root><!--COMMENT--><!--COMMENT--></root>";
            const string post = @"<root><!--" + Guid + "--><!--" + Guid + "--></root>";
            const string xpath = "//comment()";
            var task = new DerivedSetGuid();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ProcessingInstructions()
        {
            const string pre = @"<root><?foo bar?><?foo bar?></root>";
            const string post = @"<root><?foo " + Guid + "?><?foo " + Guid + "?></root>";
            const string xpath = "//processing-instruction()";
            var task = new DerivedSetGuid();
            Run(pre, post, xpath, task);
        }

        private class DerivedSetGuid : SetGuid
        {
            protected override string NewGuid()
            {
                return Guid;
            }
        }
    }
}