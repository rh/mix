using Mix.Core;
using NUnit.Framework;

namespace Mix.Tasks.Tests
{
    [TestFixture]
    public class PrettyPrintFixture
    {
        [Test]
        public void JustExecute()
        {
            var context = new Context("<root />");
            var task = new PrettyPrint();
            task.Execute(context);
        }
    }
}