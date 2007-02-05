using Mix.Core;
using NUnit.Framework;

namespace Mix.Actions.Tests
{
    public class TestFixture
    {
        private const string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>";

        public void Run(string pre, string post, string xpath, Action action)
        {
            IContext context = new Context(xml + pre, xpath);
            action.Execute(context);
            Assert.AreEqual(xml + post, context.Xml);
        }
    }
}