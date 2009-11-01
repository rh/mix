using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Trims the text nodes of the selected elements, or the value of the selected attributes, text nodes, CDATA sections, comments or processing instructions.")]
    public class Trim : TextTransformer
    {
        protected override string TransformCore(string value)
        {
            return value.Trim();
        }
    }
}