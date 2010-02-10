using System;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
	[Description("Counts all selected nodes.")]
	public class Count : Task, IReadOnly
	{
		private static int total;

		protected override void OnBeforeExecute(int count)
		{
			total += count;

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

		protected override void OnAfterAllExecute()
		{
			// TODO: let this be the result of --quiet, -verbose options etc.
			if (total > 0)
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
}