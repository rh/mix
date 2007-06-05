using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class InnerXmlActionFixture : TestFixture
    {
        [Test]
        public void Test()
        {
            const string xml = "<new>Some text</new>";
            string pre = @"<root><child>something</child></root>";
            string post = @"<root><child>" + xml + "</child></root>";
            string xpath = "//child";
            InnerXmlAction action = new InnerXmlAction();
            action.Xml = xml;
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Attribute()
        {
            const string xml = "<new>Some text</new>";
            string pre = @"<root><child attribute="""">something</child></root>";
            string post = @"<root><child attribute="""">" + xml + "</child></root>";
            string xpath = "//child/@attribute";
            InnerXmlAction action = new InnerXmlAction();
            action.Xml = xml;
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Comment()
        {
            const string xml = "<new>Some text</new>";
            string pre = @"<root><!----></root>";
            string post = @"<root><!--" + xml + "--></root>";
            string xpath = "//comment()";
            InnerXmlAction action = new InnerXmlAction();
            action.Xml = xml;
            Run(pre, post, xpath, action);
        }

        [Test]
        public void CommentValueReplaces()
        {
            const string xml = "<new>Some text</new>";
            string pre = @"<root><!--COMMENT--></root>";
            string post = @"<root><!--" + xml + "--></root>";
            string xpath = "//comment()";
            InnerXmlAction action = new InnerXmlAction();
            action.Xml = xml;
            Run(pre, post, xpath, action);
        }
    }
}