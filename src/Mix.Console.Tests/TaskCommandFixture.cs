using Mix.Console.Commands;
using Mix.Core;
using Mix.Tasks;
using NUnit.Framework;

namespace Mix.Console.Tests
{
    [TestFixture]
    public class TaskCommandFixture
    {
        [Test]
        public void Action()
        {
            ITask task = new Clear();
            TaskCommand command = new TaskCommand(task);
            Assert.AreEqual(task, command.Task);
        }

        [Test]
        public void CommandToString()
        {
            ITask task = new Clear();
            TaskCommand command = new TaskCommand(task);
            Assert.AreEqual("clear", command.ToString());
        }

        [Test]
        public void Execute()
        {
            ITask task = new Clear();
            TaskCommand command = new TaskCommand(task);
            Assert.IsTrue(command.Execute() > 0);
        }

        [Test]
        public void ExecuteWithFileNotProperlySet()
        {
            ITask task = new Clear();
            TaskCommand command = new TaskCommand(task);
            command.Context["file"] = null;
            Assert.IsTrue(command.Execute() > 0);
        }

        [Test]
        public void ExecuteWithFileSet()
        {
            ITask task = new Clear();
            TaskCommand command = new TaskCommand(task);
            // This will not actually select a file
            command.Context["file"] = "file";
            Assert.IsTrue(command.Execute() == 1);
        }

        [Test]
        public void ExecuteWithFileSetWithWrongCharacters()
        {
            ITask task = new Clear();
            TaskCommand command = new TaskCommand(task);
            command.Context["file"] = ":";
            Assert.IsTrue(command.Execute() > 0);
        }
    }
}