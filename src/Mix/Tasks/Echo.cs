using System.Xml;
using Mix.Attributes;

namespace Mix.Tasks
{
    [ReadOnly]
    public class Echo : Task
    {
        [Option, Description("The template to evaluate for every selected node.\nExample: \"Name: {name} {@id}\"")]
        public string Template { get; set; }

        protected override void ExecuteCore(XmlElement element)
        {
            var output = XPathTemplate.Evaluate(element, Template);
            Context.Output.WriteLine(output);
        }
    }
}