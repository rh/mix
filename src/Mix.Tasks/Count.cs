using System;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Counts all selected nodes.")]
    public class Count : Task, IReadOnly
    {
        protected override void OnBeforeExecute(int count)
        {
            // TODO: let this be the result of --quiet, -verbose options etc.
            if (count > 0)
            {
                var color = Console.ForegroundColor;
                Context.Output.Write("{0}: ", Context.FileName);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Context.Output.WriteLine("{0}", count);
                Console.ForegroundColor = color;
            }
        }
    }
}