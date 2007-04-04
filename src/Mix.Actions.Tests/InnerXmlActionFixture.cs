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
    }
}