using System;
using System.Collections.Generic;

namespace Mix.Commands
{
    public class AmbiguousMatchCommand : Command
    {
        private readonly string prefix;
        private readonly IList<Command> matches;

        public AmbiguousMatchCommand(string prefix, IList<Command> matches)
        {
            this.prefix = prefix;
            this.matches = matches;
        }

        public override int Execute()
        {
            WriteLine("Multiple commands start with '{0}':", prefix);

            foreach (var command in matches)
            {
                WriteLine("  {0}", command);
                WriteAliases(command);
            }

            Write(Environment.NewLine);
            WriteLine("Please respecify the command.");

            return 1; // This command is only executed when an error occurs
        }

        private void WriteAliases(Command command)
        {
            var taskCommand = command as TaskCommand;

            if (taskCommand == null)
            {
                return;
            }

            var aliases = TaskInfo.For(taskCommand.Task).Aliases;

            if (aliases.Length > 0)
            {
                WriteLine(string.Format("    ({0})", string.Join(", ", aliases)));
            }
        }
    }
}
