using System;
using System.IO;
using Mix.Console.Commands;
using Mix.Core;
using NUnit.Framework;

namespace Mix.Console.Tests
{
    [TestFixture]
    public class CommandFactoryFixture
    {
        private void TestTypeOfCommand(string[] args, Type type)
        {
            CommandFactory factory = new CommandFactory();
            Command command = factory.Create(args);
            Assert.IsNotNull(command);
            Assert.AreEqual(type, command.GetType());
        }

        private string OutputFor(Command command)
        {
            using (TextWriter writer = new StringWriter())
            {
                Context context = new Context(command.Context);
                context.Output = writer;
                command.Context = context;
                command.Execute();
                return writer.ToString();
            }
        }

        [Test]
        public void TypeOfCommand()
        {
            TestTypeOfCommand(new string[] {"help"}, typeof (HelpCommand));
            TestTypeOfCommand(new string[] {"version"}, typeof (VersionCommand));
            TestTypeOfCommand(new string[] {"foo"}, typeof (UnknownCommand));
        }

        [Test]
        public void UnknownCommand()
        {
            CommandFactory factory = new CommandFactory();
            Command command = factory.Create(new string[] {"foo"});
            Assert.AreEqual(typeof (UnknownCommand), command.GetType());
            Assert.AreEqual(OutputFor(command), OutputFor(new UnknownCommand("foo")));
        }

        [Test]
        public void VersionCommand()
        {
            CommandFactory factory = new CommandFactory();
            Command command = factory.Create(new string[] {"version"});
            Assert.AreEqual(typeof (VersionCommand), command.GetType());
            Assert.AreEqual(OutputFor(command), OutputFor(new VersionCommand()));
        }

        [Test]
        public void HelpCommand()
        {
            CommandFactory factory = new CommandFactory();
            Command command = factory.Create(new string[] {});
            Assert.AreEqual(typeof (HelpCommand), command.GetType());
            Assert.AreEqual(OutputFor(command), OutputFor(new HelpCommand()));
        }

        [Test]
        public void HelpCommand1()
        {
            CommandFactory factory = new CommandFactory();
            Command command = factory.Create(new string[] {"help"});
            Assert.AreEqual(typeof (HelpCommand), command.GetType());
            Assert.AreEqual(OutputFor(command), OutputFor(new HelpCommand()));
        }

        [Test]
        public void HelpCommand2()
        {
            CommandFactory factory = new CommandFactory();
            Command command = factory.Create(new string[] {"help", "clear"});
            Assert.AreEqual(typeof (HelpCommand), command.GetType());
            Assert.AreEqual(OutputFor(command), OutputFor(new HelpCommand(factory.Registry, "clear")));
        }

        [Test]
        public void HelpCommand3()
        {
            CommandFactory factory = new CommandFactory();
            Command command = factory.Create(new string[] {"help", "rename"});
            Assert.AreEqual(typeof (HelpCommand), command.GetType());
            Assert.AreEqual(OutputFor(command), OutputFor(new HelpCommand(factory.Registry, "rename")));
        }

        [Test]
        public void HelpCommand4()
        {
            CommandFactory factory = new CommandFactory();
            Command command = factory.Create(new string[] {"help", "foo"});
            Assert.AreEqual(typeof (HelpCommand), command.GetType());
            Assert.AreEqual(OutputFor(command), OutputFor(new HelpCommand(factory.Registry, "foo")));
        }

        [Test]
        public void ActionCommand()
        {
            CommandFactory factory = new CommandFactory();
            Command command = factory.Create(new string[] {"clear", "file:*.xml", "xpath://@*"});
            Assert.AreEqual(typeof (ActionCommand), command.GetType());
        }
    }
}