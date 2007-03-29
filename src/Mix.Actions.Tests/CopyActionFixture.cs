using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class CopyActionFixture : TestFixture
    {
        [Test]
        public void CopyElements()
        {
            //            string pre =
            //                @"<?xml version=""1.0"" encoding=""UTF-8""?>" +
            //                @"<root><pre /></root>";
            //            string post =
            //                @"<?xml version=""1.0"" encoding=""UTF-8""?>" +
            //                @"<root><post /></root>";
            //
            //            XmlDocument document = new XmlDocument();
            //            document.InnerXml = pre;
            //
            //            RenameAction renameAction = new RenameAction(document);
            //            Action action = renameAction;
            //            renameAction.Name = "post";
            //
            //            XmlNodeList nodes = document.SelectNodes("root/pre");
            //
            //            foreach (XmlNode node in nodes)
            //            {
            //                if (node is XmlElement)
            //                {
            //                    XmlElement element = (XmlElement) node;
            //                    action.Execute(element);
            //                }
            //            }
            //            Assert.AreEqual(post, document.InnerXml);
        }

        [Test]
        public void CopyAttributes()
        {
            //            string pre =
            //                @"<?xml version=""1.0"" encoding=""UTF-8""?>" +
            //                @"<root pre="""" />";
            //            string post =
            //                @"<?xml version=""1.0"" encoding=""UTF-8""?>" +
            //                @"<root post="""" />";
            //
            //            XmlDocument document = new XmlDocument();
            //            document.InnerXml = pre;
            //
            //            RenameAction renameAction = new RenameAction(document);
            //            Action action = renameAction;
            //            renameAction.Name = "post";
            //
            //            XmlNodeList nodes = document.SelectNodes("//@pre");
            //
            //            foreach (XmlNode node in nodes)
            //            {
            //                if (node is XmlAttribute)
            //                {
            //                    XmlAttribute attribute = (XmlAttribute) node;
            //                    action.Execute(attribute);
            //                }
            //            }
            //            Assert.AreEqual(post, document.InnerXml);
        }
    }
}