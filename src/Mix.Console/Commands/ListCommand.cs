using System;
using System.Collections.Generic;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Console.Commands
{
    [Description("Shows a list of all available actions.")]
    public class ListCommand : Command
    {
        public override int Execute()
        {
            WriteLine("Available actions:");
            foreach (IActionInfo info in ActionInfo.All())
            {
                string aliases = Aliases(info);
                WriteLine("  {0}{1}", info.Name, aliases);
            }
            Write(Environment.NewLine);
            WriteLine("Type 'mix help <action>' for help on a specific action.");
            return 0;
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