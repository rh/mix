using System.Xml;
using Mix.Core;
using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    public class TestFixture
    {
        public void Run(string pre, string post, string xpath, ITask task)
        {
            var document = new XmlDocument();
            document.LoadXml(pre);
            IContext context = new Context {Document = document, XPath = xpath};
            task.Execute(context);
            Assert.AreEqual(post, document.InnerXml);
        }
    }
}