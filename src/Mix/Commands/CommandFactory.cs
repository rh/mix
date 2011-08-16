using System;
using System.Collections.Generic;

namespace Mix.Commands
{
    public class CommandFactory
    {
        private readonly CommandRegistry registry = new CommandRegistry();

        /// <summary>
        /// Registers <see cref="HelpCommand"/> and <see cref="VersionCommand"/>
        /// and an <see cref="TaskCommand"/> for every known implementation of <see cref="Task"/>.
        /// </summary>
        public CommandFactory()
        {
            registry.Register(new HelpCommand());
            registry.Register(new VersionCommand());

            foreach (var info in TaskInfo.All())
            {
                Command command = new TaskCommand(info.Instance);
                registry.Register(command);
                foreach (var alias in info.Aliases)
                {
                    registry.Register(command, alias);
                }
            }
        }

        public CommandFactory(CommandRegistry registry)
        {
            this.registry = registry;
        }

        /// <summary>
        /// Creates a <see cref="Command"/> from the supplied arguments.
        /// </summary>
        /// <param name="args">The command-line arguments for this program.</param>
        /// <returns>A <see cref="Command"/> is always returned, i.e.
        /// <c>null</c> is never returned.
        /// </returns>
        public Command Create(string[] args)
        {
            var properties = Parse(args);
            var command = CreateCommand(properties, args);
            command.Context = CreateContext(properties);
            return command;
        }

        /// <summary>
        /// Creates a <see cref="Command"/> for the given name.
        /// </summary>
        /// <param name="properties">
        /// The properties which were read from the command-line arguments.
        /// </param>
        /// <param name="args">
        /// The command-line arguments for this program.
        /// </param>
        /// <returns>
        /// One of the registered commands, or an instance of <see cref="HelpCommand"/>
        /// if no name of a command was given, an instance of <see cref="UnknownCommand"/> if
        /// if the name given does not match (the first part of) the name of a
        /// registered command, or an instance of <see cref="AmbiguousMatchCommand"/>
        /// if the given name matches more than one commands.
        /// </returns>
        private Command CreateCommand(IDictionary<string, string> properties, string[] args)
        {
            if (!properties.ContainsKey("task"))
            {
                return new HelpCommand(registry);
            }

            var name = properties["task"].ToLower();

            if (name == "help")
            {
                if (args.Length <= 1)
                {
                    return new HelpCommand(registry);
                }
                return new HelpCommand(registry, args[1]);
            }

            if (registry.Contains(name))
            {
                return registry.Commands[name];
            }

            var matches = registry.Find(name);

            if (matches.Count == 0)
            {
                return new UnknownCommand(name);
            }

            if (matches.Count == 1)
            {
                return matches[0];
            }

            return new AmbiguousMatchCommand(name, matches);
        }

        /// <summary>
        /// Creates a <see cref="IContext"/> for the newly created <see cref="Command"/>.
        /// </summary>
        /// <param name="properties">
        /// </param>
        /// <returns>
        /// A new <see cref="IContext"/>, with <see cref="IContext.Output"/> and
        /// <see cref="IContext.Error"/> set to <see cref="System.Console"/>'s
        /// <see cref="System.Console.Out"/> and <see cref="System.Console.Error"/> respectively.
        /// </returns>
        private static Context CreateContext(IEnumerable<KeyValuePair<string, string>> properties)
        {
            var context = new Context(properties) {Output = System.Console.Out, Error = System.Console.Error};
            return context;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="args">
        /// The command-line arguments for this program. May be <c>null</c>.
        /// </param>
        /// <returns></returns>
        private static IDictionary<string, string> Parse(string[] args)
        {
            IDictionary<string, string> properties = new Dictionary<string, string>();

            if (args != null && args.Length > 0)
            {
                // The first argument should ALWAYS be the name of the command to invoke.
                var task = args[0];
                properties.Add("task", task);

                if (args.Length > 1)
                {
                    for (var i = 1; i < args.Length; i++)
                    {
                        var arg = args[i];
                        var name = GetName(arg);
                        var value = GetValue(arg);
                        properties.Add(name, value);
                    }
                }
            }
            return properties;
        }

        /// <summary>
        /// Gets the name of the property of a property-value pair.
        /// </summary>
        /// <param name="arg">A property-value pair.</param>
        /// <returns>
        /// The name of the property of a property-value pair.
        /// </returns>
        /// <remarks>
        /// Properties and values can be separated by ':' or '='.
        /// </remarks>
        private static string GetName(string arg)
        {
            foreach (var c in new[] {':', '='})
            {
                if (arg.Contains(c.ToString()))
                {
                    var name = arg.Split(c)[0].Replace("/", "").ToLower();
                    return name;
                }
            }
            return arg;
        }

        /// <summary>
        /// Gets the value of the property of a property-value pair.
        /// </summary>
        /// <param name="arg">A property-value pair.</param>
        /// <returns>
        /// The value of the property of a property-value pair, or
        /// <see cref="string.Empty"/> of no value has been set.
        /// </returns>
        /// <remarks>
        /// Properties and values can be separated by ':' or '='.
        /// </remarks>
        private static string GetValue(string arg)
        {
            foreach (var c in new[] {':', '='})
            {
                var index = arg.IndexOf(c);
                if (index > 0)
                {
                    return arg.Substring(index + 1);
                }
            }
            return String.Empty;
        }

        /// <summary>
        /// Gets the <see cref="CommandRegistry"/>, which contains all registered commands.
        /// </summary>
        public CommandRegistry Registry
        {
            get { return registry; }
        }
    }
}