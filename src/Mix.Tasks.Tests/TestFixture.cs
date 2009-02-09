using Mix.Core;
using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    public class TestFixture
    {
        private const string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>";

        public void Run(string pre, string post, string xpath, ITask task)
        {
            IContext context = new Context(xml + pre, xpath);
            task.Execute(context);
            Assert.AreEqual(xml + post, context.Xml);
        }
    }
}