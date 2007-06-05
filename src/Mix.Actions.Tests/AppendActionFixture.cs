using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class AppendActionFixture : TestFixture
    {
        [Test]
        public void AppendToTextElement()
        {
            const string pre = @"<root>Some text</root>";
            const string post = @"<root>Some textappend</root>";
            const string xpath = "root";
            AppendAction action = new AppendAction();
            action.Text = "append";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AppendNewElement()
        {
            const string pre = @"<root></root>";
            const string post = @"<root>append</root>";
            const string xpath = "root";
            AppendAction action = new AppendAction();
            action.Text = "append";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void PrependMixedElements()
        {
            string pre = @"<root>pre<![CDATA[pre]]>pre</root>";
            string post = @"<root>preappend<![CDATA[preappend]]>preappend</root>";
            string xpath = "root";
            AppendAction action = new AppendAction();
            action.Text = "append";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AppendToAttribute()
        {
            const string pre = @"<root attribute=""""></root>";
            const string post = @"<root attribute=""append""></root>";
            const string xpath = "root/@attribute";
            AppendAction action = new AppendAction();
            action.Text = "append";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AppendToComment()
        {
            const string pre = @"<root><!--COMMENT--></root>";
            const string post = @"<root><!--COMMENTappend--></root>";
            const string xpath = "//comment()";
            AppendAction action = new AppendAction();
            action.Text = "append";
            Run(pre, post, xpath, action);
        }
    }
}