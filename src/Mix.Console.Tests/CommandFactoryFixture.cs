using System;
using Mix.Console.Commands;
using NUnit.Framework;

namespace Mix.Console.Tests
{
    [TestFixture]
    public class CommandFactoryFixture
    {
        private void TestTypeOfCommand(string[] args, Type type)
        {
            Command command = CommandFactory.Create(args);
            Assert.IsNotNull(command);
            Assert.AreEqual(type, command.GetType());
        }

        [Test]
        public void TypeOfCommand()
        {
            TestTypeOfCommand(new string[] {"help"}, typeof(HelpCommand));
            TestTypeOfCommand(new string[] {"version"}, typeof(VersionCommand));
            TestTypeOfCommand(new string[] {"list"}, typeof(ListCommand));
            TestTypeOfCommand(new string[] {"foo"}, typeof(UnknownCommand));
            TestTypeOfCommand(new string[] {}, typeof(InfoCommand));
        }
    }
}