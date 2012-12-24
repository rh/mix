using Mix.Attributes;

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
            Context.Output.WriteLine("{0}: {1}", Context.FileName, count);
        }

        protected override void OnAfterAllExecute()
        {
            Context.Output.WriteLine();
            Context.Output.WriteLine("Total: {0}", total);
        }
    }
}
