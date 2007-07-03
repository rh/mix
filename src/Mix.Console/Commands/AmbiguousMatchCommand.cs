using System;
using System.Collections.Generic;

namespace Mix.Console.Commands
{
    public class AmbiguousMatchCommand : Command
    {
        private readonly string name;
        private readonly IList<Command> matches;

        public AmbiguousMatchCommand(string name, IList<Command> matches)
        {
            this.name = name;
            this.matches = matches;
        }

        public override int Execute()
        {
            WriteLine("Multiple actions start with '{0}':", name);
            foreach (Command command in matches)
            {
                WriteLine("  {0}", command);
            }
            Write(Environment.NewLine);

            WriteLine("Please respecify the action.");

            return 1;
        }
    }
}