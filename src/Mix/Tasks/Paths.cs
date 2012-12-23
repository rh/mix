using System.Collections.Generic;
using System.Text;
using System.Xml;
using Mix.Attributes;

namespace Mix.Tasks
{
    [ReadOnly]
    [Description("Shows all distinct XPath paths for child elements of the selected elements.")]
    public class Paths : Task
    {
        private List<string> paths = new List<string>();

        protected override void OnBeforeExecute(int count)
        {
            paths = new List<string>();
        }

        protected override void OnAfterExecute()
        {
            Context.Output.WriteLine("{0}: {1}", Context.FileName, paths.Count);
            paths.Sort(delegate(string s1, string s2) { return s1.CompareTo(s2); });

            foreach (string path in paths)
            {
                Context.Output.WriteLine("  {0}", path);
            }
        }

        protected override void ExecuteCore(XmlDocument document)
        {
            CreateXPathPaths(document, document.DocumentElement);
        }

        protected override void ExecuteCore(XmlElement element)
        {
            foreach (XmlNode child in element.ChildNodes)
            {
                if (child is XmlElement)
                {
                    CreateXPathPaths(element, child);
                }
            }
        }

        private void CreateXPathPaths(XmlNode root, XmlNode element)
        {
            var path = CreateXPathPath(root, element);

            if (!paths.Contains(path))
            {
                paths.Add(path);
            }

            foreach (XmlNode child in element.ChildNodes)
            {
                if (child is XmlElement)
                {
                    CreateXPathPaths(root, child);
                }
            }
        }

        private string CreateXPathPath(XmlNode root, XmlNode element)
        {
            var path = new StringBuilder();

            while (element != root)
            {
                path.Insert(0, "/" + element.Name);
                element = element.ParentNode;
            }

            return path.ToString().Substring(1);
        }
    }
}
