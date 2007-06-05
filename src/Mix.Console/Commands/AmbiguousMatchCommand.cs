using System;
using System.Collections.Generic;

namespace Mix.Console.Commands
{
    public class AmbiguousMatchCommand : Command
    {
        private readonly string name;
        private readonly List<string> matches;

        public AmbiguousMatchCommand(string name, List<string> matches)
        {
            this.name = name;
            this.matches = matches;
        }

        public override int Execute()
        {
            WriteLine("Multiple actions start with '{0}':", name);
            foreach (string action in matches)
            {
                WriteLine("  {0}", action);
            }
            Write(Environment.NewLine);

            WriteLine("Please respecify the action.");

            return 1;
        }
    }
}