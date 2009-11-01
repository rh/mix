using System;
using System.IO;
using System.Xml;
using Mix.Core;
using Mix.Core.Exceptions;
using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class CopyFixture : TestFixture
    {
        [Test]
        public void CopyElements()
        {
            const string pre = @"<root><child /><child /></root>";
            const string post = @"<root><child /><child /><child /><child /></root>";
            const string xpath = "//child";
            var task = new Copy();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CopyElements_CheckPosition()
        {
            const string pre = @"<root><child a=""1"" /><child a=""2"" /></root>";
            const string post = @"<root><child a=""1"" /><child a=""1"" /><child a=""2"" /><child a=""2"" /></root>";
            const string xpath = "//child";
            var task = new Copy();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CopyElementsWithChildNodes()
        {
            const string pre = @"<root><copy><child /></copy></root>";
            const string post = @"<root><copy><child /></copy><copy><child /></copy></root>";
            const string xpath = "//copy";
            var task = new Copy();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CopyText()
        {
            const string pre = @"<root>text</root>";
            const string post = @"<root>texttext</root>";
            const string xpath = "//text()";
            var task = new Copy();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CopyCDataSections()
        {
            const string pre = @"<root><![CDATA[text]]></root>";
            const string post = @"<root><![CDATA[text]]><![CDATA[text]]></root>";
            const string xpath = "//text()";
            var task = new Copy();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CopyComments()
        {
            const string pre = @"<root><!--comment--></root>";
            const string post = @"<root><!--comment--><!--comment--></root>";
            const string xpath = "//comment()";
            var task = new Copy();
            Run(pre, post, xpath, task);
        }

        [Test]
        public void CopyProcessingInstructions()
        {
            const string pre = @"<root><?pi ?></root>";
            const string post = @"<root><?pi ?><?pi ?></root>";
            const string xpath = "//processing-instruction()";
            var task = new Copy();
            Run(pre, post, xpath, task);
        }
    }
}