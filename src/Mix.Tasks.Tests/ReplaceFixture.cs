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
            const string pre = @"<root attribute=""abcdefgh""></root>";
            const string post = @"<root attribute=""abFOOgh""></root>";
            const string xpath = "root/@attribute";
            var task = new Replace {Pattern = "cdef", Replacement = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceAttributeValues()
        {
            const string pre = @"<root attribute=""abcdefgh""><child attribute=""abcdefgh"" /></root>";
            const string post = @"<root attribute=""abFOOgh""><child attribute=""abFOOgh"" /></root>";
            const string xpath = "//@attribute";
            var task = new Replace {Pattern = "cdef", Replacement = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceElementValue()
        {
            const string pre = @"<root>abcdefgh</root>";
            const string post = @"<root>abFOOgh</root>";
            const string xpath = "root";
            var task = new Replace {Pattern = "cdef", Replacement = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceNewLine()
        {
            var pre = @"<root>abcd" + Environment.NewLine + "efgh</root>";
            const string post = @"<root>abcd<br />efgh</root>";
            const string xpath = "root";
            var task = new Replace {Pattern = Environment.NewLine, Replacement = "<br />"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceNewLineFromCommandLine()
        {
            var pre = @"<root>abcd" + Environment.NewLine + "efgh</root>";
            const string post = @"<root>abcd<br />efgh</root>";
            const string xpath = "root";
            // The carriage return \r is optional, because Environment.NewLine is depends upon the platform
            var task = new Replace {Pattern = @"(\r)?\n", Replacement = "<br />"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceTab()
        {
            const string pre = "<root>abcd\tefgh</root>";
            const string post = @"<root>abcd<br />efgh</root>";
            const string xpath = "root";
            var task = new Replace {Pattern = "\t", Replacement = "<br />"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceCaseSensitive()
        {
            const string pre = "<root>abcdefgh</root>";
            const string post = "<root>abcdefgh</root>";
            const string xpath = "root";
            var task = new Replace {Pattern = "ABC", Replacement = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceCaseInsensitive()
        {
            const string pre = "<root>abcdefgh</root>";
            const string post = "<root>FOOdefgh</root>";
            const string xpath = "root";
            var task = new Replace {Pattern = "ABC", Replacement = "FOO", IgnoreCase = true};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceNonSingleline()
        {
            const string pre = "<root>abc\ndef</root>";
            const string post = pre;
            const string xpath = "root";
            var task = new Replace {Pattern = "a.*f", Replacement = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceSingleline()
        {
            const string pre = "<root>abc\ndef</root>";
            const string post = "<root>FOO</root>";
            const string xpath = "root";
            var task = new Replace {Pattern = "a.*f", Replacement = "FOO", Singleline = true};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceNonMultiline()
        {
            const string pre = "<root>abc\nabc</root>";
            const string post = pre;
            const string xpath = "root";
            var task = new Replace {Pattern = "^abc$", Replacement = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceMultiline()
        {
            const string pre = "<root>abc\nabc</root>";
            const string post = "<root>FOO\nFOO</root>";
            const string xpath = "root";
            var task = new Replace {Pattern = "^abc$", Replacement = "FOO", Multiline = true};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceElementValues()
        {
            const string pre = @"<root><child>abcdefgh</child><child>abcdefgh</child></root>";
            const string post = @"<root><child>abFOOgh</child><child>abFOOgh</child></root>";
            const string xpath = "//child";
            var task = new Replace {Pattern = "cdef", Replacement = "FOO"};
            Run(pre, post, xpath, task);
        }

        // This test only works when Replace.ExecuteCore(XmlElement) uses Task.Recurse(XmlElement).
        public void ReplaceMixedElementValues()
        {
            const string pre = @"<root><child a=""abcdefgh"">abcdefgh<![CDATA[abcdefgh]]><?foo abcdefgh?><!--abcdefgh-->abcdefgh</child></root>";
            const string post = @"<root><child a=""abFOOgh"">abFOOgh<![CDATA[abFOOgh]]><?foo abFOOgh?><!--abFOOgh-->abFOOgh</child></root>";
            const string xpath = "//child";
            var task = new Replace {Pattern = "cdef", Replacement = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceElementWithChildElements()
        {
            const string pre = @"<root><child><child-of-child>abcdefgh</child-of-child></child></root>";
            const string post = @"<root><child><child-of-child>abFOOgh</child-of-child></child></root>";
            const string xpath = "root";
            var task = new Replace {Pattern = "cdef", Replacement = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceTextNodes()
        {
            const string pre = @"<root>abcdefgh</root>";
            const string post = @"<root>abFOOgh</root>";
            const string xpath = "//text()";
            var task = new Replace {Pattern = "cdef", Replacement = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceCDataSections()
        {
            const string pre = @"<root><![CDATA[abcdefgh]]></root>";
            const string post = @"<root><![CDATA[abFOOgh]]></root>";
            const string xpath = "//text()";
            var task = new Replace {Pattern = "cdef", Replacement = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceComments()
        {
            const string pre = @"<root><!--abcdefgh--></root>";
            const string post = @"<root><!--abFOOgh--></root>";
            const string xpath = "//comment()";
            var task = new Replace {Pattern = "cdef", Replacement = "FOO"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceWithoutSettingNewValue()
        {
            const string pre = @"<root>abcdefgh</root>";
            const string post = @"<root>abgh</root>";
            const string xpath = "root";
            var task = new Replace {Pattern = "cdef"};
            Run(pre, post, xpath, task);
        }

        [Test]
        public void ReplaceProcessingInstructions()
        {
            const string pre = @"<root><?foo abcdefgh?></root>";
            const string post = @"<root><?foo abFOOgh?></root>";
            const string xpath = "//processing-instruction()";
            var task = new Replace {Pattern = "cdef", Replacement = "FOO"};
            Run(pre, post, xpath, task);
        }
    }
}