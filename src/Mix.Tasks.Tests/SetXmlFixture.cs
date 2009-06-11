using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class SetXmlFixture : TestFixture
    {
        [Test]
        public void Test()
        {
            const string xml = "<new>Some text</new>";
            const string pre = @"<root><child>something</child></root>";
            const string post = @"<root><child>" + xml + "</child></root>";
            const string xpath = "//child";
            var task = new SetXml {Xml = xml};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void Comment()
        {
            const string xml = "<new>Some text</new>";
            const string pre = @"<root><!----></root>";
            const string post = @"<root><!--" + xml + "--></root>";
            const string xpath = "//comment()";
            var task = new SetXml {Xml = xml};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CommentValueReplaces()
        {
            const string xml = "<new>Some text</new>";
            const string pre = @"<root><!--COMMENT--></root>";
            const string post = @"<root><!--" + xml + "--></root>";
            const string xpath = "//comment()";
            var task = new SetXml {Xml = xml};
            Run(pre, post, xpath, task);
        }
    }
}