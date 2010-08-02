using System;
using System.Collections.Generic;

namespace Mix.Commands
{
    public class CommandRegistry
    {
        private readonly IDictionary<string, Command> commands = new Dictionary<string, Command>();

        /// <summary>
        /// Registers <paramref name="command"/> for use in the application.
        /// </summary>
        /// <param name="command">The command to register.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="command"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// The 'name' of the <see cref="Command"/> is used as a unique key.
        /// The command's implementation of <see cref="object.ToString"/> is
        /// used for this key.
        /// </remarks>
        public void Register(Command command)
        {
            Register(command, command.ToString());
        }

        /// <summary>
        /// Registers <paramref name="command"/> for use in the application.
        /// </summary>
        /// <param name="command">The command to register.</param>
        /// <param name="name">The name or alias of the command.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="command"/> is <c>null</c>, or
        /// <paramref name="name"/> is <c>null</c> or empty.
        /// </exception>
        public void Register(Command command, string name)
        {
            if (commands.ContainsKey(name))
            {
                var message = String.Format("A command with the name or alias '{0}' is already registered.", name);
                throw new ArgumentException(message, "command");
            }
            commands[name] = command;
        }

        /// <summary>
        /// Gets a dictionary of all registered <see cref="Command"/>s.
        /// </summary>
        public IDictionary<string, Command> Commands
        {
            get { return commands; }
        }

        /// <summary>
        /// Determines if a <see cref="Command"/> exists with the supplied name.
        /// </summary>
        /// <param name="name">The name of the command.</param>
        /// <returns>
        /// <c>true</c> if a command with that name exists; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string name)
        {
            return commands.ContainsKey(name);
        }

        /// <summary>
        /// Finds all <see cref="Command"/>s whose name starts with
        /// <paramref name="prefix"/>, or just the <see cref="Command"/> whose
        /// name is equal to <paramref name="prefix"/>.
        /// </summary>
        /// <param name="prefix">The prefix of the name of the commands.</param>
        /// <returns>
        /// A list of <see cref="Command"/>s. This list may be empty, but <c>null</c>
        /// is never returned.
        /// </returns>
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