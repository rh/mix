using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Actions
{
    /// <summary>
    /// This action intentionally does nothing, because saving is already taken care of.
    /// </summary>
    [Description("Pretty prints all files.")]
    [Alias("pp")]
    public class PrettyPrint : Mix.Core.Action
    {
        /// <summary>
        /// This method just returns <c>true</c>, to indicate that the action has
        /// executed. The actual pretty-printing is already taken care of.
        /// </summary>
        /// <param name="context"></param>
        /// <returns><c>true</c>, always.</returns>
        protected override bool ExecuteCore(IContext context)
        {
            return true;
        }
    }
}