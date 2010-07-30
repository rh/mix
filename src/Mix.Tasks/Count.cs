using System;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [ReadOnly]
    [Description("Counts all selected nodes.")]
    public class Count : Task
    {
        private static int total;

        protected override void OnBeforeExecute(int count)
        {
            total += count;

            var color = Console.ForegroundColor;
            Context.Output.Write("{0}: ", Context.FileName);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Context.Output.WriteLine("{0}", count);
            Console.ForegroundColor = color;
        }

        protected override void OnAfterAllExecute()
        {
            var color = Console.ForegroundColor;
            Context.Output.WriteLine();
            Context.Output.Write("Total: ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Context.Output.WriteLine("{0}", total);
            Console.ForegroundColor = color;
        }
    }
}