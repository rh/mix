using System;
using System.IO;
using Mix.Console.Commands;
using Mix.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Mix.Console.Tests
{
    [TestFixture]
    public class CommandFactoryFixture
    {
        private static void TestTypeOfCommand(string[] args, Type type)
        {
            var factory = new CommandFactory();
            var command = factory.Create(args);
            Assert.IsNotNull(command);
            Assert.AreEqual(type, command.GetType());
        }

        private static string OutputFor(Command command)
        {
            using (TextWriter writer = new StringWriter())
            {
                var context = new Context(command.Context) {Output = writer};
                command.Context = context;
                command.Execute();
                return writer.ToString();
            }
        }

        [Test]
        public void TypeOfCommand()
        {
            TestTypeOfCommand(new[] {"help"}, typeof(HelpCommand));
            TestTypeOfCommand(new[] {"version"}, typeof(VersionCommand));
            TestTypeOfCommand(new[] {"foo"}, typeof(UnknownCommand));
        }

        [Test]
        public void UnknownCommand()
        {
            var factory = new CommandFactory();
            var command = factory.Create(new[] {"foo"});
            Assert.AreEqual(typeof(UnknownCommand), command.GetType());
            Assert.AreEqual(OutputFor(command), OutputFor(new UnknownCommand("foo")));
        }

        [Test]
        public void VersionCommand()
        {
            var factory = new CommandFactory();
            var command = factory.Create(new[] {"version"});
            Assert.AreEqual(typeof(VersionCommand), command.GetType());
            Assert.AreEqual(OutputFor(command), OutputFor(new VersionCommand()));
        }

        [Test]
        public void HelpCommand()
        {
            var factory = new CommandFactory();
            var command = factory.Create(new string[] {});
            Assert.AreEqual(typeof(HelpCommand), command.GetType());
            Assert.AreEqual(OutputFor(command), OutputFor(new HelpCommand()));
        }

        [Test]
        public void HelpCommand1()
        {
            var factory = new CommandFactory();
            var command = factory.Create(new[] {"help"});
            Assert.AreEqual(typeof(HelpCommand), command.GetType());
            Assert.AreEqual(OutputFor(command), OutputFor(new HelpCommand()));
        }

        [Test]
        public void HelpCommand2()
        {
            var factory = new CommandFactory();
            var command = factory.Create(new[] {"help", "clear"});
            Assert.AreEqual(typeof(HelpCommand), command.GetType());
            Assert.AreEqual(OutputFor(command), OutputFor(new HelpCommand(factory.Registry, "clear")));
        }

        [Test]
        public void HelpCommand3()
        {
            var factory = new CommandFactory();
            var command = factory.Create(new[] {"help", "rename"});
            Assert.AreEqual(typeof(HelpCommand), command.GetType());
            Assert.AreEqual(OutputFor(command), OutputFor(new HelpCommand(factory.Registry, "rename")));
        }

        [Test]
        public void HelpCommand4()
        {
            var factory = new CommandFactory();
            var command = factory.Create(new[] {"help", "foo"});
            Assert.AreEqual(typeof(HelpCommand), command.GetType());
            Assert.AreEqual(OutputFor(command), OutputFor(new HelpCommand(factory.Registry, "foo")));
        }

        [Test]
        public void TaskCommand()
        {
            var factory = new CommandFactory();
            var command = factory.Create(new[] {"set", "file:*.xml", "xpath://@*"});
            Assert.AreEqual(typeof(TaskCommand), command.GetType());
        }

        [Test]
        public void AmbiguousMatchCommand()
        {
            var registry = new CommandRegistry();
            registry.Register(new BarCommand());
            registry.Register(new BazCommand());
            var factory = new CommandFactory(registry);
            var command = factory.Create(new[] {"ba"});
            Assert.That(command, Is.InstanceOfType(typeof(AmbiguousMatchCommand)));
        }

        [Test]
        public void OutputForAmbiguousMatchCommand()
        {
            var registry = new CommandRegistry();
            registry.Register(new BarCommand());
            registry.Register(new BazCommand());
            var factory = new CommandFactory(registry);
            var command = factory.Create(new[] {"ba"});
            var output = OutputFor(command);
            Assert.That(output, Text.Contains("Multiple commands start with 'ba':"));
            Assert.That(output, Text.Contains("  bar"));
            Assert.That(output, Text.Contains("  baz"));
        }

        private class BarCommand : Command
        {
        }

        private class BazCommand : Command
        {
        }
    }
}