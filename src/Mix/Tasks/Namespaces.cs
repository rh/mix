using System.Xml;
using Mix.Attributes;

namespace Mix.Tasks
{
    [ReadOnly]
    [Description("Shows a list of namespaces, including prefixes.")]
    public class Namespaces : Task
    {
        protected override bool ExecuteCore(Context context)
        {
            context.Output.Write("{0}: ", context.FileName);

            var manager = CreateNamespaceManager(Context.Document);
            var namespaces = manager.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml);
            context.Output.WriteLine(namespaces.Count);
            foreach (var pair in namespaces)
            {
                context.Output.WriteLine("  {0,-12} {1}", pair.Key, pair.Value);
            }

            return true;
        }

        /// <summary>
        /// Creates a <see cref="XmlNamespaceManager"/> for <paramref name="document"/>.
        /// Namespaces declared in the document node are automatically added.
        /// The default namespace is given the prefix 'default'.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private static XmlNamespaceManager CreateNamespaceManager(XmlDocument document)
        {
            var manager = new XmlNamespaceManager(document.NameTable);

            foreach (XmlNode node in document.SelectNodes("//node()"))
            {
                if (node is XmlElement)
                {
                    var element = node as XmlElement;
                    foreach (XmlAttribute attribute in element.Attributes)
                    {
                        if (attribute.Name == "xmlns")
                        {
                            // The first default namespace wins
                            // (since using multiple default namespaces in a single file is not considered a good practice)
                            if (!manager.HasNamespace("default"))
                            {
                                manager.AddNamespace("default", attribute.Value);
                            }
                        }
                        if (attribute.Prefix == "xmlns")
                        {
                            manager.AddNamespace(attribute.LocalName, attribute.Value);
                        }
                    }
                }
            }

            return manager;
        }
    }
}