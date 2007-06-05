using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mix.Core;

namespace Mix.Console.Commands
{
    public class HelpCommand : Command
    {
        private string name = String.Empty;
        private IDictionary<string, Command> commands =
            new Dictionary<string, Command>();

        #region Constructors

        public HelpCommand()
        {
        }

        public HelpCommand(IDictionary<string, Command> commands)
        {
            this.commands = commands;
        }

        public HelpCommand(IDictionary<string, Command> commands, string name)
        {
            this.commands = commands;
            this.name = name;
        }

        #endregion

        public override int Execute()
        {
            if (ActionIsNotSet)
            {
                WriteUsage();
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

        private bool ActionIsKnown
        {
            get { return commands.ContainsKey(name); }
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
            WriteLine("  mix set file:test.xml xpath://foo text:\"Some text\"");

            WriteActions();

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
                string aliases = Aliases(info);
                WriteLine("  {0}{1}", info.Name, aliases);
            }
        }

        private void WriteActionUsage()
        {
            object obj = commands[name];

            if (obj is ActionCommand)
            {
                obj = (obj as ActionCommand).Action;
            }

            IActionInfo info = ActionInfo.For(obj);
            WriteLine("{0}: {1}", name, info.Description);

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
            WriteLine("Type 'mix list' to see a list of all available actions.");
        }

        private string Aliases(IActionInfo info)
        {
            string result = String.Empty;

            IList<string> aliases = info.Aliases;

            if (aliases.Count > 0)
            {
                foreach (string alias in aliases)
                {
                    if (result != String.Empty)
                    {
                        result = result + ", ";
                    }
                    result = result + alias;
                }
                result = "\n    (" + result + ")";
            }

            return result;
        }
    }
}