using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Counts all selected nodes.")]
    public class Count : Task, IReadOnly
    {
        protected override void OnBeforeExecute(int count)
        {
            // TODO: let this be the result of --quiet, -verbose arguments etc.
            if (count > 0)
            {
                Context.Output.WriteLine("{0}: {1}", Context.FileName, count);
            }
        }
    }
}