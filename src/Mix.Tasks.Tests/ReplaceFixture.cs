using System;
using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class ReplaceFixture : TestFixture
    {
        [Test]
        public void ReplaceAttributeValue()
        {
            var pre = @"<root attribute=""abcdefgh""></root>";
            var post = @"<root attribute=""abFOOgh""></root>";
            var xpath = "root/@attribute";
            var task = new Replace {OldValue = "cdef", NewValue = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceAttributeValues()
        {
            var pre = @"<root attribute=""abcdefgh""><child attribute=""abcdefgh"" /></root>";
            var post = @"<root attribute=""abFOOgh""><child attribute=""abFOOgh"" /></root>";
            var xpath = "//@attribute";
            var task = new Replace {OldValue = "cdef", NewValue = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceElementValue()
        {
            var pre = @"<root>abcdefgh</root>";
            var post = @"<root>abFOOgh</root>";
            var xpath = "root";
            var task = new Replace {OldValue = "cdef", NewValue = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceNewLine()
        {
            var pre = @"<root>abcd" + Environment.NewLine + "efgh</root>";
            var post = @"<root>abcd<br />efgh</root>";
            var xpath = "root"; // &#xa;&#xd;
            var task = new Replace {OldValue = Environment.NewLine, NewValue = "<br />"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceNewLineFromCommandLine()
        {
            var pre = @"<root>abcd" + Environment.NewLine + "efgh</root>";
            var post = @"<root>abcd<br />efgh</root>";
            var xpath = "root"; // &#xa;&#xd;
            var task = new Replace {OldValue = "\\n", NewValue = "<br />"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceElementValues()
        {
            var pre = @"<root><child>abcdefgh</child><child>abcdefgh</child></root>";
            var post = @"<root><child>abFOOgh</child><child>abFOOgh</child></root>";
            var xpath = "//child";
            var task = new Replace {OldValue = "cdef", NewValue = "FOO"};
            Run(pre, post, xpath, task);
        }

        // This test only works when Replace.ExecuteCore(XmlElement) uses Task.Recurse(XmlElement).
        public void ReplaceMixedElementValues()
        {
            var pre = @"<root><child a=""abcdefgh"">abcdefgh<![CDATA[abcdefgh]]><?foo abcdefgh?><!--abcdefgh-->abcdefgh</child></root>";
            var post = @"<root><child a=""abFOOgh"">abFOOgh<![CDATA[abFOOgh]]><?foo abFOOgh?><!--abFOOgh-->abFOOgh</child></root>";
            var xpath = "//child";
            var task = new Replace {OldValue = "cdef", NewValue = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceElementWithChildElements()
        {
            var pre = @"<root><child><child-of-child>abcdefgh</child-of-child></child></root>";
            var post = @"<root><child><child-of-child>abFOOgh</child-of-child></child></root>";
            var xpath = "root";
            var task = new Replace {OldValue = "cdef", NewValue = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceTextNodes()
        {
            var pre = @"<root>abcdefgh</root>";
            var post = @"<root>abFOOgh</root>";
            var xpath = "//text()";
            var task = new Replace {OldValue = "cdef", NewValue = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceCDataSections()
        {
            var pre = @"<root><![CDATA[abcdefgh]]></root>";
            var post = @"<root><![CDATA[abFOOgh]]></root>";
            var xpath = "//text()";
            var task = new Replace {OldValue = "cdef", NewValue = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceComments()
        {
            var pre = @"<root><!--abcdefgh--></root>";
            var post = @"<root><!--abFOOgh--></root>";
            var xpath = "//comment()";
            var task = new Replace {OldValue = "cdef", NewValue = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceWithoutSettingNewValue()
        {
            var pre = @"<root>abcdefgh</root>";
            var post = @"<root>abgh</root>";
            var xpath = "root";
            var task = new Replace {OldValue = "cdef"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceProcessingInstructions()
        {
            var pre = @"<root><?foo abcdefgh?></root>";
            var post = @"<root><?foo abFOOgh?></root>";
            var xpath = "//processing-instruction()";
            var task = new Replace {OldValue = "cdef", NewValue = "FOO"};
            Run(pre, post, xpath, task);
        }
    }
}