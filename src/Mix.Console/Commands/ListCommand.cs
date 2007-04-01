using System;
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
                WriteLine("  {0}", info.Name);
            }
            Write(Environment.NewLine);
            WriteLine("Type 'mix help <action>' for help on a specific action.");
            return 0;
        }
    }
}