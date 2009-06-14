using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Appends text to selected nodes (if they 'behave' like text), or to the value of the selected attributes.")]
    public class Append : TextTransformer
    {
        [Option, Required]
        [Description("The value to append.")]
        public string Value { get; set; }

        protected override string TransformCore(string value)
        {
            return value + Value;
        }
    }
}