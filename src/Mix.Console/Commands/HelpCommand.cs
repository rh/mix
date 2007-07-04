using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
            if (ActionIsNotSet)
            {
                WriteUsage();
            }
            else if (ActionIsAmbiguous)
            {
                WriteAmbiguousActionUsage();
                return 1;
            }
            else if (ActionIsKnown)
            {
                WriteActionUsage();
            }
            else
            {
                WriteUnknownActionUsage();
                return 1;
            }
            return 0;
        }

        private bool ActionIsNotSet
        {
            get { return String.IsNullOrEmpty(name); }
        }

        private bool ActionIsAmbiguous
        {
            get { return registry.Find(name).Count > 1; }
        }

        private bool ActionIsKnown
        {
            get { return registry.Find(name).Count == 1; }
        }

        private void WriteUsage()
        {
            WriteLine("Usage: mix <action> [arguments]");
            WriteLine("Mix command-line client, version {0}.", Application.ProductVersion);
            WriteLine("Type 'mix help <action>' for help on a specific action.");
            WriteLine("Type 'mix version' to see the program version.");
            Write(Environment.NewLine);

            WriteLine("Most actions take 'file' and/or 'xpath' arguments.");
            WriteLine("If 'file' is not set the value '*.xml' is used.");
            Write(Environment.NewLine);

            WriteLine("Examples:");
            WriteLine("  mix addattribute file:test.xml xpath://node() name:id");
            WriteLine("  mix set file:test.xml xpath://foo value:\"Some text\"");

            WriteActions();

            Write(Environment.NewLine);
            WriteLine("Actions marked with * do not change files.");

            Write(Environment.NewLine);
            WriteLine("Mix is a tool for XML refactoring.");
            WriteLine("For additional information, see http://mix.sourceforge.net/");
        }

        private void WriteActions()
        {
            Write(Environment.NewLine);
            WriteLine("Available actions:");
            foreach (IActionInfo info in ActionInfo.All())
            {
                string readOnly = info.Instance is IReadOnly ? "*" : "";
                string aliases = Aliases(info);
                WriteLine("  {0}{1}{2}", info.Name, readOnly, aliases);
            }
        }

        private void WriteAmbiguousActionUsage()
        {
            IList<Command> matches = registry.Find(name);
            AmbiguousMatchCommand command = new AmbiguousMatchCommand(name, matches);
            command.Context = Context;
            command.Execute();
        }

        private void WriteActionUsage()
        {
            object obj = registry.Find(name)[0];

            if (obj is ActionCommand)
            {
                obj = (obj as ActionCommand).Action;
            }

            IActionInfo info = ActionInfo.For(obj);
            WriteLine("{0}: {1}", obj, info.Description);

            if (info.Arguments.Length > 0)
            {
                Write(Environment.NewLine);
                WriteLine("Arguments:");
                foreach (IArgumentInfo argument in info.Arguments)
                {
                    WriteLine("  {0,-15}{1}", argument.Name.ToLower(), argument.Description);
                    if (argument.Required)
                    {
                        WriteLine("                 [required]");
                    }
                }
            }
        }

        private void WriteUnknownActionUsage()
        {
            WriteLine("Unknown action: '{0}'", name);
            WriteLine("Type 'mix help' to see a list of all available actions.");
        }

        private string Aliases(IActionInfo info)
        {
            if (info.Aliases.Length > 0)
            {
                return String.Format("{1}    ({0})", String.Join(", ", info.Aliases), Environment.NewLine);
            }
            else
            {
                return String.Empty;
            }
        }
    }
}