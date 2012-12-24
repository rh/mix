using System;
using System.Collections.Generic;
using System.Xml;
using Mix.Attributes;

namespace Mix.Tasks
{
    [ReadOnly]
    [Description("Shows all distinct values of the selected nodes.")]
    public class Distinct : Task
    {
        private List<string> values = new List<string>();

        public Distinct()
        {
            Text = true;
        }

        [Option]
        [Description("If set, the textual representation of elements will be compared. This is the default.\nExample: when selecting <div> elements with the XPath expression '//div[p]', the value '<p>See <a href=\"http://www.example.org/\">here</a></p>' will be compared as 'See here'.")]
        public bool Text { get; set; }

        [Option]
        [Description("If set, the inner XML of elements will be compared.\nExample: when selecting <div> elements with the XPath expression '//div[p]', the value '<p>See <a href=\"http://www.example.org/\">here</a></p>' will be compared as such.")]
        public bool InnerXml { get; set; }

        [Option]
        [Description("If set, the outer XML of elements, that is the XML including the selected element, will be compared.\nExample: when selecting <div> elements with the XPath expression '//div[p]', the value '<div><p>See <a href=\"http://www.example.org/\">here</a></p></div>' will be compared as such.")]
        public bool OuterXml { get; set; }

        protected override void OnBeforeExecute(int count)
        {
            values = new List<string>();
        }

        protected override void OnAfterExecute()
        {
            Context.Quiet.WriteLine("{0}: {1}", Context.FileName, values.Count);
            values.Sort((s1, s2) => s1.CompareTo(s2));

            foreach (var value in values)
            {
                Context.Output.WriteLine(value);
            }
        }

        private void AddValue(string value)
        {
            if (!values.Contains(value))
            {
                values.Add(value);
            }
        }

        protected override void ExecuteCore(XmlElement element)
        {
            if (InnerXml)
            {
                AddValue(element.InnerXml);
            }
            else if (OuterXml)
            {
                AddValue(element.OuterXml);
            }
            else
            {
                AddValue(element.InnerText);
            }
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            AddValue(attribute.Value);
        }
    }
}
