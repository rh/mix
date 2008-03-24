using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class ReplaceActionFixture : TestFixture
    {
        [Test]
        public void ReplaceAttributeValue()
        {
            string pre = @"<root attribute=""abcdefgh""></root>";
            string post = @"<root attribute=""abFOOgh""></root>";
            string xpath = "root/@attribute";
            ReplaceAction action = new ReplaceAction();
            action.OldValue = "cdef";
            action.NewValue = "FOO";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ReplaceAttributeValues()
        {
            string pre = @"<root attribute=""abcdefgh""><child attribute=""abcdefgh"" /></root>";
            string post = @"<root attribute=""abFOOgh""><child attribute=""abFOOgh"" /></root>";
            string xpath = "//@attribute";
            ReplaceAction action = new ReplaceAction();
            action.OldValue = "cdef";
            action.NewValue = "FOO";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ReplaceElementValue()
        {
            string pre = @"<root>abcdefgh</root>";
            string post = @"<root>abFOOgh</root>";
            string xpath = "root";
            ReplaceAction action = new ReplaceAction();
            action.OldValue = "cdef";
            action.NewValue = "FOO";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ReplaceElementValues()
        {
            string pre = @"<root><child>abcdefgh</child><child>abcdefgh</child></root>";
            string post = @"<root><child>abFOOgh</child><child>abFOOgh</child></root>";
            string xpath = "//child";
            ReplaceAction action = new ReplaceAction();
            action.OldValue = "cdef";
            action.NewValue = "FOO";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ReplaceMixedElementValues()
        {
            string pre = @"<root><child a=""abcdefgh"">abcdefgh<![CDATA[abcdefgh]]><?foo abcdefgh?><!--abcdefgh-->abcdefgh</child></root>";
            string post = @"<root><child a=""abFOOgh"">abFOOgh<![CDATA[abFOOgh]]><?foo abFOOgh?><!--abFOOgh-->abFOOgh</child></root>";
            string xpath = "//child";
            ReplaceAction action = new ReplaceAction();
            action.OldValue = "cdef";
            action.NewValue = "FOO";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ReplaceElementWithChildElements()
        {
            string pre = @"<root><child><child-of-child>abcdefgh</child-of-child></child></root>";
            string post = @"<root><child><child-of-child>abFOOgh</child-of-child></child></root>";
            string xpath = "root";
            ReplaceAction action = new ReplaceAction();
            action.OldValue = "cdef";
            action.NewValue = "FOO";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ReplaceTextNodes()
        {
            string pre = @"<root>abcdefgh</root>";
            string post = @"<root>abFOOgh</root>";
            string xpath = "//text()";
            ReplaceAction action = new ReplaceAction();
            action.OldValue = "cdef";
            action.NewValue = "FOO";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ReplaceCDataSections()
        {
            string pre = @"<root><![CDATA[abcdefgh]]></root>";
            string post = @"<root><![CDATA[abFOOgh]]></root>";
            string xpath = "//text()";
            ReplaceAction action = new ReplaceAction();
            action.OldValue = "cdef";
            action.NewValue = "FOO";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ReplaceComments()
        {
            string pre = @"<root><!--abcdefgh--></root>";
            string post = @"<root><!--abFOOgh--></root>";
            string xpath = "//comment()";
            ReplaceAction action = new ReplaceAction();
            action.OldValue = "cdef";
            action.NewValue = "FOO";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ReplaceWithoutSettingNewValue()
        {
            string pre = @"<root>abcdefgh</root>";
            string post = @"<root>abgh</root>";
            string xpath = "root";
            ReplaceAction action = new ReplaceAction();
            action.OldValue = "cdef";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void ReplaceProcessingInstructions()
        {
            string pre = @"<root><?foo abcdefgh?></root>";
            string post = @"<root><?foo abFOOgh?></root>";
            string xpath = "//processing-instruction()";
            ReplaceAction action = new ReplaceAction();
            action.OldValue = "cdef";
            action.NewValue = "FOO";
            Run(pre, post, xpath, action);
        }
    }
}