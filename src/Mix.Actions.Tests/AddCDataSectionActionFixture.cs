using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class AddCDataSectionActionFixture : TestFixture
    {
        [Test]
        public void AddToElement()
        {
            const string pre = @"<root></root>";
            const string post = @"<root><![CDATA[text]]></root>";
            const string xpath = "root";
            AddCdataSection action = new AddCdataSection();
            action.Value = "text";
            Run(pre, post, xpath, action);
        }
    }
}