using System;
using System.Collections.Generic;
using Mix.Actions;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Console.Commands
{
    public static class CommandFactory
    {
        private static IDictionary<string, Command> commands =
            new Dictionary<string, Command>();

        static CommandFactory()
        {
            // TODO: replace this with dynamic registration

            Register(new HelpCommand());
            Register(new VersionCommand());
            Register(new ListCommand());
            Register(new ActionCommand(new ExtractAction()));
            Register(new ActionCommand(new ClearAction()));
            Register(new ActionCommand(new AddAttributeAction()));
            Register(new ActionCommand(new LowerCaseAction()));
            Register(new ActionCommand(new PrependAction()));
            Register(new ActionCommand(new RemoveAction()));
            Register(new ActionCommand(new RenameAction()));
            Register(new ActionCommand(new AppendAction()));
            Register(new ActionCommand(new UpperCaseAction()));
            Register(new ActionCommand(new ReplaceAction()));
            Register(new ActionCommand(new CopyAction()));
            Register(new ActionCommand(new CopyAttributeAction()));
            Register(new ActionCommand(new InnerXmlAction()));
            Register(new ActionCommand(new CountAction()));
        }

        /// <summary>
        /// Creates a <see cref="Command"/> from the supplied args.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>A <see cref="Command"/> is always returned, i.e.
        /// <c>null</c> is never returned.
        /// </returns>
        public static Command Create(string[] args)
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
                        command = new HelpCommand();
                    }
                    else
                    {
                        command = new HelpCommand(args[1]);
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
        private static IDictionary<string, string> Parse(string[] args)
        {
            IDictionary<string, string> properties = new Dictionary<string, string>();

            if (args != null && args.Length > 0)
            {
                // The first argument should ALWAYS be the name of the action
                // to invoke
                string action = args[0];
                properties.Add("action", action);

                for (int i = 1; i < args.Length; i++)
                {
                    string arg = args[i];
                    string name = arg.Split(':')[0].Replace("/", "").ToLower();
                    int index = arg.IndexOf(':');
                    string value;
                    if (index > 0)
                    {
                        value = arg.Substring(index + 1);
                    }
                    else
                    {
                        value = String.Empty;
                    }

                    properties.Add(name, value);
                }
            }
            return properties;
        }

        /// <summary>
        /// Registers <paramref name="command"/> for use in the application.
        /// </summary>
        /// <param name="command"></param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="command"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// The 'name' of the <see cref="Command"/> is used as a unique key.
        /// If the <see cref="Command"/> implements <see cref="IName"/>, then
        /// <see cref="IName.Name"/> is used as the name of the <see cref="Command"/>.
        /// If the <see cref="Command"/> is decorated with a <see cref="NameAttribute"/>,
        /// then <see cref="NameAttribute.Name"/> is used.
        /// Otherwise, the name of the <see cref="Type"/> is used, in lowercase, minus the
        /// word 'command', so for <see cref="HelpCommand"/> a name of 'help'
        /// will be used.
        /// </remarks>
        public static void Register(Command command)
        {
            Check.ArgumentIsNotNull(command, "command");

            string name = command.GetType().Name.ToLower().Replace("command", "");

            if (NameAttribute.IsDefinedOn(command))
            {
                name = NameAttribute.GetNameFrom(command);
            }

            if (command is IName)
            {
                name = (command as IName).Name;
            }

            if (commands.ContainsKey(name))
            {
                string message =
                    String.Format("A command with the name '{0}' is already registered.",
                                  name);
                throw new ArgumentException(message, "command");
            }
            commands[name] = command;
        }

        public static IDictionary<string, Command> Commands
        {
            get { return commands; }
        }
    }
}