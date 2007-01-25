using System.Collections.Generic;
using System.Xml;
using Mix.Core;
using NUnit.Framework;

namespace Mix.Actions.Tests
{
    public class TestFixture
    {
        private const string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>";

        public void Run(string pre, string post, string xpath, Action action)
        {
            IList<Action> actions = new List<Action>();
            actions.Add(action);
            Run(pre, post, xpath, actions);
        }

        public void Run(string pre, string post, string xpath, IList<Action> actions)
        {
            XmlDocument document = new XmlDocument();
            document.InnerXml = xml + pre;

            // TODO: actually do something with this XmlNamespaceManager
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(document.NameTable);
            IContext context = new Context(document, xpath, null, namespaceManager);

            Executor executor = new Executor(context, actions);
            executor.Execute();
            Assert.AreEqual(xml + post, executor.Xml);
        }
    }
}
