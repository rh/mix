using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Makes (part of) the value of all selected nodes lowercase.")]
    public class LowerCase : TextTransformer
    {
        protected override string TransformCore(string value)
        {
            return value.ToLower();
        }
    }
}