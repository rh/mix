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
            Register(new ListCommand());

            foreach (IActionInfo info in ActionInfo.All())
            {
                Action action = (Action) Activator.CreateInstance(info.Type);
                Register(new ActionCommand(action));
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

            Command command = new InfoCommand();

            if (properties.ContainsKey("action"))
            {
                string name = properties["action"].ToLower();

                if (name == "help")
                {
                    if (args.Length <= 1)
                    {
                        command = new HelpCommand(Commands);
                    }
                    else
                    {
                        command = new HelpCommand(Commands, args[1]);
                    }
                }
                else if (commands.ContainsKey(name))
                {
                    command = commands[name];
                }
                else
                {
                    command = new UnknownCommand(name);
                }
            }

            Context context = new Context(properties);
            context.Output = System.Console.Out;
            context.Error = System.Console.Error;
            command.Context = context;
            return command;
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
                // to invoke
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

            string name = command.ToString();

            if (commands.ContainsKey(name))
            {
                string message =
                    String.Format("A command with the name '{0}' is already registered.",
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