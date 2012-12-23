using System;
using System.Collections.Generic;

namespace Mix.Commands
{
    public class CommandRegistry
    {
        private readonly IDictionary<string, Command> commands = new Dictionary<string, Command>();

        public void Register(Command command)
        {
            Register(command, command.ToString());
        }

        public void Register(Command command, string name)
        {
            if (commands.ContainsKey(name))
            {
                var message = string.Format("A command with the name or alias '{0}' is already registered.", name);
                throw new ArgumentException(message, "command");
            }

            commands[name] = command;
        }

        public IDictionary<string, Command> Commands
        {
            get { return commands; }
        }

        public bool Contains(string name)
        {
            return commands.ContainsKey(name);
        }

        public IList<Command> Find(string prefix)
        {
            IList<Command> matches = new List<Command>();

            // If the prefix is actually the name or alias of a command,
            // return that command.
            if (commands.ContainsKey(prefix))
            {
                matches.Add(commands[prefix]);

                return matches;
            }

            foreach (var key in commands.Keys)
            {
                if (key.StartsWith(prefix))
                {
                    var command = commands[key];

                    if (!matches.Contains(command))
                    {
                        matches.Add(command);
                    }
                }
            }

            return matches;
        }
    }
}
