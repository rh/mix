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
            var registry = new CommandRegistry();
            registry.Register(new HelpCommand());
            Assert.AreEqual(1, registry.Commands.Count);
            Assert.IsTrue(registry.Contains("help"));
        }

        [Test]
        public void RegisterTheSameCommandTwice()
        {
            var registry = new CommandRegistry();
            registry.Register(new HelpCommand());

            Assert.Throws<ArgumentException>(() => registry.Register(new HelpCommand()));
        }

        [Test]
        public void RegisterBothCommandAndAlias()
        {
            var registry = new CommandRegistry();
            registry.Register(new HelpCommand());
            registry.Register(new HelpCommand(), "h");
            Assert.AreEqual(2, registry.Commands.Count);
            Assert.IsTrue(registry.Contains("help"));
            Assert.IsTrue(registry.Contains("h"));
        }

        [Test]
        public void RegisterBothCommandAndAliasAndFindWithPrefix()
        {
            var registry = new CommandRegistry();
            registry.Register(new HelpCommand());
            registry.Register(new HelpCommand(), "h");
            // Only finds the alias, because the prefix and the name of the
            // command are the same in this case.
            Assert.AreEqual(1, registry.Find("h").Count);
        }

        [Test]
        public void FindShouldReturnTheSameCommandJustOnce()
        {
            var registry = new CommandRegistry();
            registry.Register(new HelpCommand());
            registry.Register(new HelpCommand(), "hlp");
            Assert.AreEqual(1, registry.Find("h").Count);
        }
    }
}