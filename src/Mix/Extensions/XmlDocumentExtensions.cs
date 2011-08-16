using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using Mix.Exceptions;

namespace Mix.Extensions
{
    public static class XmlDocumentExtensions
    {
        public static IList<XmlNode> Select(this XmlDocument document, string xpath)
        {
            var manager = CreateNamespaceManager(document);

            try
            {
                var nodes = new List<XmlNode>();
                foreach (XmlNode node in document.SelectNodes(xpath, manager))
                {
                    nodes.Add(node);
                }
                return nodes;
            }
            catch (XPathException e)
            {
                var message = String.Format("'{0}' is not a valid XPath expression.", xpath);
                throw new TaskExecutionException(message, e);
            }
        }

        /// <summary>
        /// Creates a <see cref="XmlNamespaceManager"/> for <paramref name="document"/>.
        /// Namespaces declared in the document node are automatically added.
        /// The default namespace is given the prefix 'ns'.
        /// </summary>
        public static XmlNamespaceManager CreateNamespaceManager(this XmlDocument document)
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
                            if (!manager.HasNamespace("ns"))
                            {
                                manager.AddNamespace("ns", attribute.Value);
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