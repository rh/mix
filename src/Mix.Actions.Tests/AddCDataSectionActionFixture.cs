using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class AddCDataSectionActionFixture : TestFixture
    {
        [Test]
        public void AddToElement()
        {
            const string pre = @"<root></root>";
            const string post = @"<root><![CDATA[text]]></root>";
            const string xpath = "root";
            AddCDataSectionAction action = new AddCDataSectionAction();
            action.Text = "text";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AddToAttribute()
        {
            const string pre = @"<root a=""""></root>";
            const string post = @"<root a=""""><![CDATA[text]]></root>";
            const string xpath = "root/@a";
            AddCDataSectionAction action = new AddCDataSectionAction();
            action.Text = "text";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AddToTextNode()
        {
            const string pre = @"<root>text</root>";
            const string post = @"<root>text<![CDATA[text]]></root>";
            const string xpath = "//text()";
            AddCDataSectionAction action = new AddCDataSectionAction();
            action.Text = "text";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AddToCDataSection()
        {
            const string pre = @"<root><![CDATA[text]]></root>";
            const string post = @"<root><![CDATA[texttext]]></root>";
            const string xpath = "//text()";
            AddCDataSectionAction action = new AddCDataSectionAction();
            action.Text = "text";
            Run(pre, post, xpath, action);
        }
    }
}