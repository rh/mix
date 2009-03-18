using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Mix.Core;

namespace Mix.Console.Commands
{
    public class HelpCommand : Command
    {
        private readonly string name = String.Empty;
        private readonly CommandRegistry registry;

        public HelpCommand()
        {
        }

        public HelpCommand(CommandRegistry registry)
        {
            this.registry = registry;
        }

        public HelpCommand(CommandRegistry registry, string name)
        {
            this.registry = registry;
            this.name = name;
        }

        public override int Execute()
        {
            if (TaskIsNotSet)
            {
                WriteUsage();
            }
            else if (TaskIsAmbiguous)
            {
                WriteAmbiguousTaskUsage();
                return 1;
            }
            else if (TaskIsKnown)
            {
                WriteTaskUsage();
            }
            else
            {
                WriteUnknownTaskUsage();
                return 1;
            }
            return 0;
        }

        private bool TaskIsNotSet
        {
            get { return String.IsNullOrEmpty(name); }
        }

        private bool TaskIsAmbiguous
        {
            get { return registry.Find(name).Count > 1; }
        }

        private bool TaskIsKnown
        {
            get { return registry.Find(name).Count == 1; }
        }

        private void WriteUsage()
        {
            WriteLine("Usage: mix <command> [arguments]");
            WriteLine("Mix command-line client, version {0}.", Assembly.GetExecutingAssembly().GetName().Version);
            WriteLine("Type 'mix help <command>' for help on a specific command.");
            WriteLine("Type 'mix version' to see the program version.");
            Write(Environment.NewLine);

            WriteLine("Most commands take 'file' and/or 'xpath' arguments.");
            WriteLine("If 'file' is not set the value '*.xml' is used.");
            Write(Environment.NewLine);

            WriteLine("Examples:");
            WriteLine("  mix add-attribute file:test.xml xpath://node() name:id");
            WriteLine("  mix set file:test.xml xpath://foo value:\"Some text\"");

            WriteTasks();

            Write(Environment.NewLine);
            WriteLine("Commands marked with * do not change files.");

            Write(Environment.NewLine);
            WriteLine("Mix is a tool for XML refactoring.");
            WriteLine("For additional information, see http://mix.sourceforge.net/");
        }

        private void WriteTasks()
        {
            Write(Environment.NewLine);
            WriteLine("Available commands:");
            foreach (var info in TaskInfo.All())
            {
                var readOnly = info.Instance is IReadOnly ? "*" : "";
                var aliases = Aliases(info);
                WriteLine("  {0}{1}{2}", info.Name, readOnly, aliases);
            }
        }

        private void WriteAmbiguousTaskUsage()
        {
            var matches = registry.Find(name);
            var command = new AmbiguousMatchCommand(name, matches) {Context = Context};
            command.Execute();
        }

        private void WriteTaskUsage()
        {
            object obj = registry.Find(name)[0];

            if (obj is TaskCommand)
            {
                obj = (obj as TaskCommand).Task;
            }

            var info = TaskInfo.For(obj);
            WriteLine("{0}: {1}", obj, info.Description);

            if (info.Arguments.Length > 0)
            {
                Write(Environment.NewLine);
                WriteLine("Arguments:");
                foreach (var argument in info.Arguments)
                {
                    var description = argument.Description;
                    Write("  {0,-18}", argument.Name.ToLower());
                    if (description.Length > System.Console.WindowWidth - 20)
                    {
                        var parts = Wrap(description, System.Console.WindowWidth - 22);
                        for (var i = 0; i < parts.Length; i++)
                        {
                            var part = parts[i];
                            if (i == 0)
                            {
                                WriteLine(part);
                            }
                            else
                            {
                                WriteLine("                    {0}", part);
                            }
                        }
                    }
                    else
                    {
                        WriteLine(description);
                    }
                    if (argument.Required)
                    {
                        WriteLine("                    [required]");
                    }
                }
            }
        }

        private void WriteUnknownTaskUsage()
        {
            WriteLine("Unknown command: '{0}'", name);
            WriteLine("Type 'mix help' to see a list of all available commands.");
        }

        private static string Aliases(ITaskInfo info)
        {
            if (info.Aliases.Length > 0)
            {
                return String.Format(" ({0})", String.Join(", ", info.Aliases));
            }
            return String.Empty;
        }

        private static string[] Wrap(string value, int length)
        {
            StringBuilder builder;
            var list = new List<string>();

            var lines = value.Split('\n');
            foreach (var line in lines)
            {
                builder = new StringBuilder();
                var parts = line.Split(' ');
                foreach (var part in parts)
                {
                    if (builder.Length + part.Length + 1 < length)
                    {
                        builder.AppendFormat("{0} ", part);
                    }
                    else
                    {
                        list.Add(builder.ToString());
                        builder = new StringBuilder();
                        builder.AppendFormat("{0} ", part);
                    }
                }

                if (builder.Length > 0)
                {
                    list.Add(builder.ToString());
                }
            }

            return list.ToArray();
        }
    }
}