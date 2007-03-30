using System;
using Mix.Core;

namespace Mix.Console.Commands
{
    public class HelpCommand : Command
    {
        private string name = String.Empty;

        #region Constructors

        public HelpCommand()
        {
        }

        public HelpCommand(string name)
        {
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
            get { return CommandFactory.Commands.ContainsKey(name); }
        }

        private void WriteUsage()
        {
            WriteLine("Usage: mix <action> [arguments]");
            Write(Environment.NewLine);
            WriteLine("Mix is a tool for XML refactoring.");
            WriteLine("For additional information, see http://mix.sourceforge.net/");
            Write(Environment.NewLine);
            WriteLine("Type 'mix version' to see the program version.");
            WriteLine("Type 'mix list' to see a list of all available actions.");
            WriteLine("Type 'mix help <action>' for help on a specific action.");
        }

        private void WriteActionUsage()
        {
            object obj = CommandFactory.Commands[name];

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
    }
}