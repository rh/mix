using NUnit.Framework;

namespace Mix.Actions.Tests
{
    [TestFixture]
    public class AddProcessingInstructionActionFixture : TestFixture
    {
        [Test]
        public void AddToElementWithoutValue()
        {
            const string pre = @"<root></root>";
            const string post = @"<root><?name ?></root>";
            const string xpath = "root";
            AddProcessingInstructionAction action = new AddProcessingInstructionAction();
            action.Name = "name";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void AddToElement()
        {
            const string pre = @"<root></root>";
            const string post = @"<root><?name value?></root>";
            const string xpath = "root";
            AddProcessingInstructionAction action = new AddProcessingInstructionAction();
            action.Name = "name";
            action.Value = "value";
            Run(pre, post, xpath, action);
        }

        [Test]
        public void NoSelection()
        {
            const string pre = @"<root></root>";
            const string post = pre;
            const string xpath = "foo";
            AddProcessingInstructionAction action = new AddProcessingInstructionAction();
            action.Name = "name";
            action.Value = "value";
            Run(pre, post, xpath, action);
        }
    }
}