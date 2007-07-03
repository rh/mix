using System;
using Mix.Console.Commands;
using NUnit.Framework;

namespace Mix.Console.Tests
{
    [TestFixture]
    public class CommandRegistryFixture
    {
        [Test]
        public void RegisterOne()
        {
            CommandRegistry registry = new CommandRegistry();
            registry.Register(new HelpCommand());
            Assert.AreEqual(1, registry.Commands.Count);
            Assert.IsTrue(registry.Contains("help"));
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void RegisterTheSameCommandTwice()
        {
            CommandRegistry registry = new CommandRegistry();
            registry.Register(new HelpCommand());
            registry.Register(new HelpCommand());
        }

        [Test]
        public void RegisterBothCommandAndAlias()
        {
            CommandRegistry registry = new CommandRegistry();
            registry.Register(new HelpCommand());
            registry.Register(new HelpCommand(), "h");
            Assert.AreEqual(2, registry.Commands.Count);
            Assert.IsTrue(registry.Contains("help"));
            Assert.IsTrue(registry.Contains("h"));
        }

        [Test]
        public void RegisterBothCommandAndAliasAndFindWithPrefix()
        {
            CommandRegistry registry = new CommandRegistry();
            registry.Register(new HelpCommand());
            registry.Register(new HelpCommand(), "h");
            // Only finds the alias, because the prefix and the name of the
            // command are the same in this case.
            Assert.AreEqual(1, registry.Find("h").Count);
        }
    }
}