using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class TrimActionFixture : TestFixture
    {
        [Test]
        public void Elements()
        {
            string pre = @"<root><node> some text  </node></root>";
            string post = @"<root><node>some text</node></root>";
            string xpath = "//node";
            TrimAction action = new TrimAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Attributes()
        {
            string pre = @"<root attribute="" some text  ""></root>";
            string post = @"<root attribute=""some text""></root>";
            string xpath = "//@*";
            TrimAction action = new TrimAction();
            Run(pre, post, xpath, action);
        }

        [Test]
        public void Comments()
        {
            string pre = @"<root><!-- COMMENT  --><!--   COMMENT --></root>";
            string post = @"<root><!--COMMENT--><!--COMMENT--></root>";
            string xpath = "//comment()";
            TrimAction action = new TrimAction();
            Run(pre, post, xpath, action);
        }
    }
}