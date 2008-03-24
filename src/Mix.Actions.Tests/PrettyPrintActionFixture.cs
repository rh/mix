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
            PrettyPrintAction action = new PrettyPrintAction();
            action.Execute(context);
        }
    }
}