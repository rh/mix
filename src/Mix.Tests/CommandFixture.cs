using System;
using System.IO;
using Mix.Commands;
using NUnit.Framework;

namespace Mix.Tests
{
    [TestFixture]
    public class CommandFixture
    {
        [Test]
        public void ContextNull()
        {
            Command command = new HelpCommand();
            Assert.AreEqual(Context.Null, command.Context);
        }

        [Test]
        public void NullContext()
        {
            Assert.Throws<ArgumentNullException>(() => new HelpCommand {Context = null});
        }

        [Test]
        public void Execute()
        {
            Command command = new EmptyCommand();
            Assert.AreEqual(0, command.Execute());
        }

        [Test]
        public void Write()
        {
            Command command = new EmptyCommand();
            var context = new Context();
            var writer = new StringWriter();
            context.Output = writer;
            command.Context = context;
            command.Execute();
            var expected = String.Format("12{0}3{0}", Environment.NewLine);
            Assert.AreEqual(expected, writer.ToString());
        }

        [Test]
        public void CommandToString()
        {
            Command command = new EmptyCommand();
            Assert.AreEqual("empty", command.ToString());
        }

        [Test]
        public void TwoInstancesOfOneTypeOfCommandShouldBeEqual()
        {
            var command1 = new HelpCommand();
            var command2 = new HelpCommand();
            Assert.AreEqual(command1, command2);
        }

        [Test]
        public void TwoTypesOfCommandShouldNotBeEqual()
        {
            var helpCommand = new HelpCommand();
            var versionCommand = new VersionCommand();
            Assert.AreNotEqual(helpCommand, versionCommand);
        }

        private class EmptyCommand : Command
        {
            public override int Execute()
            {
                Write("1");
                WriteLine("2");
                WriteLine("{0}", 3);
                return base.Execute();
            }
        }
    }
}