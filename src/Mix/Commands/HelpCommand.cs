using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Mix.Attributes;

namespace Mix.Commands
{
    public class HelpCommand : Command
    {
        private const int LeftMargin = 21;

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
            WriteLine("Usage: mix <command> [options] <xpath> <file> [file]");
            WriteLine("Mix command-line client, version {0}.", Assembly.GetExecutingAssembly().GetName().Version);
            WriteLine("Type 'mix help <command>' for help on a specific command.");
            WriteLine("Type 'mix version' to see the program version.");
            Write(Environment.NewLine);

            WriteLine("Examples:");
            WriteLine("  mix count / a.xml b.xml c.xml");
            WriteLine("  mix count / *.xml");
            WriteLine("  mix count / a.xml{0}b.xml", Path.PathSeparator);
            WriteLine("  mix add-attribute --name id \"//node()\" test.xml");
            WriteLine("  mix set --value bar //foo test.xml");

            WriteTasks();

            Write(Environment.NewLine);
            WriteLine("Commands marked with * do not change files.");

            Write(Environment.NewLine);
            WriteLine("Mix is a tool for XML refactoring.");
            WriteLine("For additional information, see http://rh.github.com/mix");
        }

        private void WriteTasks()
        {
            Write(Environment.NewLine);
            WriteLine("Available commands:");

            foreach (var info in TaskInfo.All())
            {
                var readOnly = ReadOnlyAttribute.IsDefinedOn(info.Instance) ? "*" : "";
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
            var taskDescription = String.Format("{0}: {1}", obj, info.Description);
            var taskParts = Wrap(taskDescription, Console.WindowWidth);

            for (var i = 0; i < taskParts.Length; i++)
            {
                WriteLine(taskParts[i]);
            }

            Write(Environment.NewLine);
            WriteLine("Options:");
            WriteOptionName("debug");
            Write("   ");
            WriteOptionDescription("If set, debug information is shown.");
            WriteOptionName("recursive");
            Write("   ");
            WriteOptionDescription("If set, all files matching the specified name or pattern are processed recursively.\nIf not set, only the current or a given directory is searched.");

            foreach (var option in info.Options)
            {
                WriteOptionName(option.Name.ToLower());

                if (option.Required)
                {
                    Write(" * ");
                }
                else
                {
                    Write("   ");
                }

                WriteOptionDescription(option.Description);
            }
        }

        private void WriteOptionName(string name)
        {
            Write("  --{0,-14}", name);
        }

        private void WriteOptionDescription(string description)
        {
            var parts = Wrap(description, Console.WindowWidth - LeftMargin);

            for (var i = 0; i < parts.Length; i++)
            {
                var part = parts[i];

                if (i == 0)
                {
                    WriteLine(part);
                }
                else
                {
                    WriteLine("{0}{1}", new string(' ', LeftMargin), part);
                }
            }
        }

        private void WriteUnknownTaskUsage()
        {
            WriteLine("Unknown command: '{0}'", name);
            WriteLine("Type 'mix help' to see a list of all available commands.");
        }

        private static string Aliases(TaskInfo info)
        {
            if (info.Aliases.Length > 0)
            {
                return String.Format(" ({0})", String.Join(", ", info.Aliases));
            }

            return string.Empty;
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
                    if (builder.Length + part.Length + 1 <= length)
                    {
                        builder.AppendFormat("{0} ", part);
                    }
                    else
                    {
                        list.Add(builder.ToString().TrimEnd());
                        builder = new StringBuilder();
                        builder.AppendFormat("{0} ", part);
                    }
                }

                if (builder.Length > 0)
                {
                    list.Add(builder.ToString().TrimEnd());
                }
            }

            return list.ToArray();
        }
    }
}
