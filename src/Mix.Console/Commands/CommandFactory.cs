using System;
using System.Collections.Generic;
using Mix.Core;

namespace Mix.Console.Commands
{
    public class CommandFactory
    {
        private static IDictionary<string, Command> commands =
            new Dictionary<string, Command>();

        static CommandFactory()
        {
            Register(new HelpCommand());
            Register(new VersionCommand());

            foreach (IActionInfo info in ActionInfo.All())
            {
                Command command = new ActionCommand(info.Instance);
                Register(command);
                foreach (string alias in info.Aliases)
                {
                    Register(command, alias);
                }
            }
        }

        /// <summary>
        /// Creates a <see cref="Command"/> from the supplied args.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>A <see cref="Command"/> is always returned, i.e.
        /// <c>null</c> is never returned.
        /// </returns>
        public Command Create(string[] args)
        {
            Check.ArgumentIsNotNull(args, "args");

            IDictionary<string, string> properties = Parse(args);

            Command command = CreateCommand(properties, args);
            command.Context = CreateContext(properties);
            return command;
        }

        private Command CreateCommand(IDictionary<string, string> properties, string[] args)
        {
            if (!properties.ContainsKey("action"))
            {
                return new InfoCommand();
            }

            string name = properties["action"].ToLower();

            if (name == "help")
            {
                if (args.Length <= 1)
                {
                    return new HelpCommand(Commands);
                }
                else
                {
                    return new HelpCommand(Commands, args[1]);
                }
            }
            else if (commands.ContainsKey(name))
            {
                return commands[name];
            }
            else
            {
                List<string> matches = new List<string>();

                foreach (string key in commands.Keys)
                {
                    if (key.StartsWith(name))
                    {
                        matches.Add(key);
                    }
                }

                if (matches.Count == 0)
                {
                    return new UnknownCommand(name);
                }
                else if (matches.Count == 1)
                {
                    return commands[matches[0]];
                }
                else
                {
                    return new AmbiguousMatchCommand(name, matches);
                }
            }
        }

        public IContext CreateContext(IDictionary<string, string> properties)
        {
            Context context = new Context(properties);
            context.Output = System.Console.Out;
            context.Error = System.Console.Error;
            return context;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="args">
        /// The command-line to parse. May be <c>null</c>.
        /// </param>
        /// <returns></returns>
        private IDictionary<string, string> Parse(string[] args)
        {
            IDictionary<string, string> properties = new Dictionary<string, string>();

            if (args != null && args.Length > 0)
            {
                // The first argument should ALWAYS be the name of the action
                // to invoke.
                string action = args[0];
                properties.Add("action", action);

                if (args.Length > 1)
                {
                    for (int i = 1; i < args.Length; i++)
                    {
                        string arg = args[i];
                        string name = GetName(arg);
                        string value = GetValue(arg);
                        properties.Add(name, value);
                    }
                }
            }
            return properties;
        }

        private string GetName(string arg)
        {
            foreach (char c in ":=")
            {
                if (arg.Contains(c.ToString()))
                {
                    string name = arg.Split(c)[0].Replace("/", "").ToLower();
                    return name;
                }
            }
            return arg;
        }

        private string GetValue(string arg)
        {
            foreach (char c in ":=")
            {
                int index = arg.IndexOf(c);
                if (index > 0)
                {
                    return arg.Substring(index + 1);
                }
            }
            return String.Empty;
        }

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
        public static void Register(Command command)
        {
            Check.ArgumentIsNotNull(command, "command");

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
        public static void Register(Command command, string name)
        {
            Check.ArgumentIsNotNull(command, "command");
            Check.ArgumentIsNotNullOrEmpty(name, "name");

            if (commands.ContainsKey(name))
            {
                string message =
                    String.Format("A command with the name or alias '{0}' is already registered.",
                                  name);
                throw new ArgumentException(message, "command");
            }
            commands[name] = command;
        }

        public IDictionary<string, Command> Commands
        {
            get { return commands; }
        }
    }
}