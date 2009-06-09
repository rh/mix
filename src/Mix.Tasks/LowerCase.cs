using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Makes (part of) the value of all selected nodes lowercase.")]
    public class LowerCase : TextTransformer
    {
        protected override string DoTransform(string value)
        {
            return value.ToLower();
        }
    }
}