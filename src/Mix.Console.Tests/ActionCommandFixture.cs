using Mix.Actions;
using Mix.Console.Commands;
using Mix.Core;
using NUnit.Framework;

namespace Mix.Console.Tests
{
    [TestFixture]
    public class ActionCommandFixture
    {
        [Test]
        public void Action()
        {
            IAction action = new Clear();
            ActionCommand command = new ActionCommand(action);
            Assert.AreEqual(action, command.Action);
        }

        [Test]
        public void CommandToString()
        {
            IAction action = new Clear();
            ActionCommand command = new ActionCommand(action);
            Assert.AreEqual("clear", command.ToString());
        }

        [Test]
        public void Execute()
        {
            IAction action = new Clear();
            ActionCommand command = new ActionCommand(action);
            Assert.IsTrue(command.Execute() > 0);
        }

        [Test]
        public void ExecuteWithFileNotProperlySet()
        {
            IAction action = new Clear();
            ActionCommand command = new ActionCommand(action);
            command.Context["file"] = null;
            Assert.IsTrue(command.Execute() > 0);
        }

        [Test]
        public void ExecuteWithFileSet()
        {
            IAction action = new Clear();
            ActionCommand command = new ActionCommand(action);
            // This will not actually select a file
            command.Context["file"] = "file";
            Assert.IsTrue(command.Execute() == 1);
        }

        [Test]
        public void ExecuteWithFileSetWithWrongCharacters()
        {
            IAction action = new Clear();
            ActionCommand command = new ActionCommand(action);
            command.Context["file"] = ":";
            Assert.IsTrue(command.Execute() > 0);
        }
    }
}