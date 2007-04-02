using System;
using System.IO;
using Mix.Console.Commands;
using Mix.Core;
using NUnit.Framework;

namespace Mix.Console.Tests
{
    [TestFixture]
    public class CommandFixture
    {
        [Test]
        public void ContextNull()
        {
            Command command = new InfoCommand();
            Assert.AreEqual(Context.Null, command.Context);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullContext()
        {
            Command command = new InfoCommand();
            command.Context = null;
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
            Context context = new Context();
            StringWriter writer = new StringWriter();
            context.Output = writer;
            command.Context = context;
            command.Execute();
            string expected = String.Format("12{0}3{0}", Environment.NewLine);
            Assert.AreEqual(expected, writer.ToString());
        }

        [Test]
        public void CommandToString()
        {
            Command command = new EmptyCommand();
            Assert.AreEqual("empty", command.ToString());
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