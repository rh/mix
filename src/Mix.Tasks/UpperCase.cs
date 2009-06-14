using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Makes (part of) the value of all selected nodes uppercase.")]
    public class UpperCase : TextTransformer
    {
        protected override string TransformCore(string value)
        {
            return value.ToUpper();
        }
    }
}