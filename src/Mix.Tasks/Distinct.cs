using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Shows all distinct values of the selected nodes.")]
    public class Distinct : Task, IReadOnly
    {
        private List<string> values = new List<string>();

        protected override void OnBeforeExecute(int count)
        {
            values = new List<string>();
        }

        protected override void OnAfterExecute()
        {
            Context.Output.WriteLine("{0}: {1}", Context.FileName, values.Count);
            values.Sort(delegate(string s1, string s2) { return s1.CompareTo(s2); });
            foreach (string value in values)
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

        /// <remarks>
        /// This calls InnerText on the element which means the textual representation of the 
        /// element is compared with other values. As a result
        /// <code>
        /// &lt;p>Foo &lt;img src="" />bar&lt;/p>
        /// </code>
        /// will be considered the same as
        /// <code>
        /// &lt;p>Foo &lt;a href="">bar&lt;/a>&lt;/p>
        /// </code>
        /// </remarks>
        protected override void ExecuteCore(XmlElement element)
        {
            AddValue(element.InnerText);
        }

        protected override void ExecuteCore(XmlAttribute attribute)
        {
            AddValue(attribute.Value);
        }
    }
}