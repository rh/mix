using System;
using System.IO;
using System.Collections.Generic;

namespace Mix.Commands
{
    public class CommandFactory
    {
        private readonly CommandRegistry registry = new CommandRegistry();

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

        public Command Create(string[] args)
        {
            var properties = Parse(args);
            var context = CreateContext(properties);
            var command = CreateCommand(properties, args);
            command.Context = context;

            foreach (var arg in args)
            {
                context.Debug.WriteLine("Debug: arg: '{0}'", arg);
            }

            return command;
        }

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

        private static Context CreateContext(IEnumerable<KeyValuePair<string, string>> properties)
        {
            var context = new Context(properties) {Output = System.Console.Out, Error = System.Console.Error};

            foreach (var pair in properties)
            {
                if (pair.Key == "debug")
                {
                    context.Debug = System.Console.Out;
                }
            }

            return context;
        }

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
                    string name = null;
                    string value = null;

                    for (var i = 1; i < args.Length; i++)
                    {
                        var arg = args[i];

                        if (arg.StartsWith("--"))
                        {
                            if (name != null)
                            {
                                properties.Add(name, null);
                            }

                            name = arg.Substring(2);
                            continue;
                        }

                        value = arg;

                        if (name != null)
                        {
                            properties.Add(name, value);
                        }
                        else if (!properties.ContainsKey("xpath"))
                        {
                            properties.Add("xpath", value);
                        }
                        else if (!properties.ContainsKey("file"))
                        {
                            properties.Add("file", value);
                        }
                        else
                        {
                            properties["file"] = properties["file"] + Path.PathSeparator + value;
                        }

                        name = null;
                        value = null;
                    }

                    if (name != null)
                    {
                        properties.Add(name, null);
                    }
                }
            }

            return properties;
        }

        public CommandRegistry Registry
        {
            get { return registry; }
        }
    }
}
