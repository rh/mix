using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Prepends text to selected nodes (if they 'behave' like text), or to the value of the selected attributes.")]
    public class Prepend : TextTransformer
    {
        [Option, Required]
        [Description("The value to prepend.")]
        public string Value { get; set; }

        protected override string TransformCore(string value)
        {
            return Value + value;
        }
    }
}