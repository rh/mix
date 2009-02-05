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
            Context context = new Context("<root />");
            PrettyPrint action = new PrettyPrint();
            action.Execute(context);
        }
    }
}