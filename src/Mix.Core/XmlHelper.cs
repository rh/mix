using System;
using System.Text;
using System.Xml;

namespace Mix.Core
{
    public static class XmlHelper
    {
        /// <summary>
        /// Creates a <see cref="XmlNamespaceManager"/> for <paramref name="document"/>.
        /// Namespaces declared in the document node are automatically added.
        /// The default namespace is given the prefix 'default'.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static XmlNamespaceManager CreateNamespaceManager(XmlDocument document)
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="document"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <c>document</c> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <c>attribute</c> is <c>null</c>.
        /// </exception>
        public static XmlAttribute CopyAttribute(XmlDocument document, XmlAttribute attribute)
        {
            var newattribute = document.CreateAttribute(attribute.Name);
            newattribute.Value = attribute.Value;
            return newattribute;
        }

        public static void CopyAttributes(XmlDocument document, XmlElement from, XmlElement to)
        {
            foreach (XmlAttribute attribute in from.Attributes)
            {
                to.Attributes.Append(CopyAttribute(document, attribute));
            }
        }

        public static void CopyChildNodes(XmlElement from, XmlElement to)
        {
            foreach (XmlNode node in from.ChildNodes)
            {
                to.AppendChild(node.CloneNode(true));
            }
        }
    }
}