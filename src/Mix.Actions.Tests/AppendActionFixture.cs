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
            Append action = new Append();
            action.Value = "append";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AppendNewElement()
        {
            const string pre = @"<root></root>";
            const string post = @"<root>append</root>";
            const string xpath = "root";
            Append action = new Append();
            action.Value = "append";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AppendMixedElements()
        {
            string pre = @"<root>pre<![CDATA[pre]]><?foo bar?><!--comment-->pre</root>";
            string post = @"<root>preappend<![CDATA[preappend]]><?foo barappend?><!--commentappend-->preappend</root>";
            string xpath = "root";
            Append action = new Append();
            action.Value = "append";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AppendToAttribute()
        {
            const string pre = @"<root attribute=""""></root>";
            const string post = @"<root attribute=""append""></root>";
            const string xpath = "root/@attribute";
            Append action = new Append();
            action.Value = "append";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AppendToTextNode()
        {
            const string pre = @"<root>text</root>";
            const string post = @"<root>textappend</root>";
            const string xpath = "//text()";
            Append action = new Append();
            action.Value = "append";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AppendToCDataSection()
        {
            const string pre = @"<root><![CDATA[pre]]></root>";
            const string post = @"<root><![CDATA[preappend]]></root>";
            const string xpath = "//root";
            Append action = new Append();
            action.Value = "append";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AppendToComment()
        {
            const string pre = @"<root><!--COMMENT--></root>";
            const string post = @"<root><!--COMMENTappend--></root>";
            const string xpath = "//comment()";
            Append action = new Append();
            action.Value = "append";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AppendToProcessingInstruction()
        {
            const string pre = @"<root><?foo bar?></root>";
            const string post = @"<root><?foo barappend?></root>";
            const string xpath = "//processing-instruction()";
            Append action = new Append();
            action.Value = "append";
            Run(pre, post, xpath, action);
        }
    }
}