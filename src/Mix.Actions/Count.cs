using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    /// <summary>
    /// Counts and shows the number of selected nodes.
    /// </summary>
    [Description("Counts all selected nodes.")]
    public class Count : Task, IReadOnly
    {
        /// <summary>
        /// Outputs the name of the file and the number of selected nodes
        /// to <see cref="IContext.Output"/>.
        /// </summary>
        /// <param name="count">
        /// The number of selected nodes.
        /// </param>
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