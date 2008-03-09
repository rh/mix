using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    [Description("Counts all selected nodes.")]
    public class CountAction : Action, IReadOnly
    {
        protected override void OnBeforeExecute(int count)
        {
            Context.Output.WriteLine("{0}: {1}", Context.FileName, count);
        }
    }
}