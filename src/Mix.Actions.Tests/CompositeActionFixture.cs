using System.Collections.Generic;
using Mix.Core;
using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class CompositeActionFixture : TestFixture
    {
        [Test]
        public void Elements()
        {
            string pre = @"<root><child /></root>";
            string post = @"<root><new post="""" /></root>";
            string xpath = "root/child";
            IList<Action> actions = new List<Action>();
            actions.Add(new AddAttributeAction("post"));
            actions.Add(new RenameAction("new"));
            Run(pre, post, xpath, actions);
        }
    }
}
