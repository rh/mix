using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    public class Echo : Task, IReadOnly
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