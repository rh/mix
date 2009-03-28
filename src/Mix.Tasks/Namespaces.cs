using System;
using System.Xml;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Tasks
{
    [Description("Shows a list of namespaces, including prefixes.")]
    public class Namespaces : Task, IReadOnly
    {
        protected override bool ExecuteCore(IContext context)
        {
            context.Output.Write("{0}: ", context.FileName);

            var ForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;

            var manager = XmlHelper.CreateNamespaceManager(Document);
            var namespaces = manager.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml);
            context.Output.WriteLine(namespaces.Count);
            foreach (var pair in namespaces)
            {
                context.Output.WriteLine("  {0,-12} {1}", pair.Key, pair.Value);
            }

            Console.ForegroundColor = ForegroundColor;

            return true;
        }
    }
}