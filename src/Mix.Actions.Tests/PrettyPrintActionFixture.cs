using Mix.Core;
using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class PrettyPrintActionFixture
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