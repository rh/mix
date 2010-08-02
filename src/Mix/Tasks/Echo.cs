using System.Xml;
using Mix.Attributes;

namespace Mix.Tasks
{
    [ReadOnly]
    public class Echo : Task
    {
        [Option, Description("")]
        public string Template { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            var output = Evaluate(element, Template);
            Context.Output.WriteLine(output);
        }
    }
}