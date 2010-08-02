using System;
using System.Collections.Generic;

namespace Mix.Commands
{
    /// <summary>
    /// Mix can be called on the command-line with just the first part of the
    /// name of a <see cref="Command"/>. However, if multiple <see cref="Command"/>s
    /// match the given name, this <see cref="Command"/> is executed.
    /// </summary>
    public class AmbiguousMatchCommand : Command
    {
        private readonly string prefix;
        private readonly IList<Command> matches;

        /// <summary>
        /// Constructs and initializes a new instance of the <see cref="AmbiguousMatchCommand"/>
        /// class.
        /// </summary>
        /// <param name="prefix">
        /// The first part of the name of several <see cref="Command"/>s.
        /// </param>
        /// <param name="matches">
        /// A list of <see cref="Command"/>s whose name starts with <paramref name="prefix"/>.
        /// </param>
        public AmbiguousMatchCommand(string prefix, IList<Command> matches)
        {
            this.prefix = prefix;
            this.matches = matches;
        }

        /// <summary>
        /// Prints the names of all matched <see cref="Command"/>s.
        /// </summary>
        /// <returns>
        /// This method always returns 1, meaning an error occurred, because
        /// this <see cref="Command"/> is only executed in the case of an error.
        /// </returns>
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
            return 1;
        }

        /// <summary>
        /// Writes the aliases, if any, to the output stream.
        /// </summary>
        /// <param name="command">
        /// The <see cref="Command"/> for which aliases should be shown.
        /// </param>
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
                WriteLine(String.Format("    ({0})", String.Join(", ", aliases)));
            }
        }
    }
}