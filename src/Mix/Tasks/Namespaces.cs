using System.Xml;
using Mix.Attributes;
using Mix.Extensions;

namespace Mix.Tasks
{
    [ReadOnly]
    [Description("Shows a list of namespaces, including prefixes.")]
    public class Namespaces : Task
    {
        protected override bool ExecuteCore(Context context)
        {
            context.Quiet.Write("{0}: ", context.FileName);

            var manager = Context.Document.CreateNamespaceManager();
            var namespaces = manager.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml);
            context.Quiet.WriteLine(namespaces.Count);

            foreach (var pair in namespaces)
            {
                context.Output.WriteLine("  {0,-12} {1}", pair.Key, pair.Value);
            }

            return true;
        }
    }
}
