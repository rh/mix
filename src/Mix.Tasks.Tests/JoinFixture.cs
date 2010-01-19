using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class JoinFixture : TestFixture
    {
        // target: empty/text/xml
        // source: empty/single node/multiple nodes
        // source: text/inner xml/outer xml
        // separator or not

        [Test]
        public void JoinEmptySourceWithEmptyTarget()
        {
            const string Pre = @"<root><target></target><source></source></root>";
            const string Post = @"<root><target></target></root>";
            const string XPath = "//target";
            var task = new Join {With = "../source", Text = true};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void JoinMultipleEmptySourcesWithEmptyTarget()
        {
            const string Pre = @"<root><target></target><source></source><source></source><source></source></root>";
            const string Post = @"<root><target></target></root>";
            const string XPath = "//target";
            var task = new Join {With = "../source", Text = true};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void JoinEmptySourceWithTextTarget()
        {
            const string Pre = @"<root><target>foo</target><source></source></root>";
            const string Post = @"<root><target>foo</target></root>";
            const string XPath = "//target";
            var task = new Join {With = "../source", Text = true};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void JoinMultipleEmptySourcesWithTextTarget()
        {
            const string Pre = @"<root><target>foo</target><source></source><source></source><source></source></root>";
            const string Post = @"<root><target>foo</target></root>";
            const string XPath = "//target";
            var task = new Join {With = "../source", Text = true};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void JoinEmptySourceWithXmlTarget()
        {
            const string Pre = @"<root><target><xml /></target><source></source></root>";
            const string Post = @"<root><target><xml /></target></root>";
            const string XPath = "//target";
            var task = new Join {With = "../source", Text = true};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void JoinTextWithEmptyTarget()
        {
            const string Pre = @"<root><target></target><source>text</source></root>";
            const string Post = @"<root><target>text</target></root>";
            const string XPath = "//target";
            var task = new Join {With = "../source", Text = true};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void JoinTextWhenTextAlreadyPresent()
        {
            const string Pre = @"<root><target>foo</target><source>text</source></root>";
            const string Post = @"<root><target>footext</target></root>";
            const string XPath = "//target";
            var task = new Join {With = "../source", Text = true};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void JoinTextWithMultipleNodes()
        {
            const string Pre = @"<root><target></target><source>text</source><source>text</source><source>text</source></root>";
            const string Post = @"<root><target>texttexttext</target></root>";
            const string XPath = "//target";
            var task = new Join {With = "../source", Text = true};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void TextWithSeparator()
        {
            const string Pre = @"<root><target></target><source>text</source><source>text</source><source>text</source></root>";
            const string Post = @"<root><target>text;text;text</target></root>";
            const string XPath = "//target";
            var task = new Join {With = "../source", Text = true, Separator = ";"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void TextWithSeparatorWithTextAlreadyPresent()
        {
            const string Pre = @"<root><target>foo</target><source>text</source><source>text</source><source>text</source></root>";
            const string Post = @"<root><target>foo;text;text;text</target></root>";
            const string XPath = "//target";
            var task = new Join {With = "../source", Text = true, Separator = ";"};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void InnerXml()
        {
            const string Pre = @"<root><target></target><source><a /><b /><c /></source></root>";
            const string Post = @"<root><target><a /><b /><c /></target></root>";
            const string XPath = "//target";
            var task = new Join {With = "../source", InnerXml = true};
            Run(Pre, Post, XPath, task);
        }

        [Test]
        public void OuterXml()
        {
            const string Pre = @"<root><target></target><source><a /><b /><c /></source></root>";
            const string Post = @"<root><target><source><a /><b /><c /></source></target></root>";
            const string XPath = "//target";
            var task = new Join {With = "../source", OuterXml = true};
            Run(Pre, Post, XPath, task);
        }
    }
}