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
			XmlNamespaceManager manager = new XmlNamespaceManager(document.NameTable);
			foreach (XmlAttribute attribute in document.SelectSingleNode("/*").Attributes)
			{
				if (attribute.Name == "xmlns")
				{
					manager.AddNamespace("default", attribute.Value);
				}
				if (attribute.Prefix == "xmlns")
				{
					manager.AddNamespace(attribute.LocalName, attribute.Value);
				}
			}
			return manager;
		}

		/// <summary>
		/// Removes the XML declaration from <paramref name="xml"/>.
		/// </summary>
		/// <param name="xml"></param>
		/// <returns></returns>
		public static string RemoveXmlDeclaration(string xml)
		{
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);
			StringBuilder builder = new StringBuilder();
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.OmitXmlDeclaration = true;
			using (XmlWriter writer = XmlWriter.Create(builder, settings))
			{
				document.WriteContentTo(writer);
			}
			return builder.ToString();
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="document"></param>
		/// <param name="element"></param>
		/// <param name="name"></param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <c>document</c> is <c>null</c>, or <c>element</c>
		/// is <c>null</c>, or  <c>name</c> is <c>null</c> or empty.
		/// </exception>
		public static void AddAttribute(XmlDocument document, XmlElement element, string name)
		{
			AddAttribute(document, element, name, null);
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="document"></param>
		/// <param name="element"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <exception cref="ArgumentNullException">
		/// Thrown when <c>document</c> is <c>null</c>, or <c>element</c>
		/// is <c>null</c>, or  <c>name</c> is <c>null</c> or empty.
		/// </exception>
		public static void AddAttribute(XmlDocument document, XmlElement element, string name, string value)
		{
			Check.ArgumentIsNotNull(document, "document");
			Check.ArgumentIsNotNull(element, "element");
			Check.ArgumentIsNotNullOrEmpty(name, "name");

			if (element.HasAttribute(name))
			{
				return;
			}

			XmlAttribute attribute = document.CreateAttribute(name);
			if (value != null && value.Length > 0)
			{
				attribute.Value = value;
			}
			element.Attributes.Append(attribute);
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
			Check.ArgumentIsNotNull(document, "document");
			Check.ArgumentIsNotNull(attribute, "attribute");

			XmlAttribute newattribute = document.CreateAttribute(attribute.Name);
			newattribute.Value = attribute.Value;
			return newattribute;
		}

		public static void CopyAttributes(XmlDocument document, XmlElement from, XmlElement to)
		{
			Check.ArgumentIsNotNull(document, "document");
			Check.ArgumentIsNotNull(from, "from");
			Check.ArgumentIsNotNull(to, "to");

			foreach (XmlAttribute attribute in from.Attributes)
			{
				to.Attributes.Append(CopyAttribute(document, attribute));
			}
		}

		public static void CopyChildNodes(XmlElement from, XmlElement to)
		{
			Check.ArgumentIsNotNull(from, "from");
			Check.ArgumentIsNotNull(to, "to");

			foreach (XmlNode node in from.ChildNodes)
			{
				to.AppendChild(node.CloneNode(true));
			}
		}
	}
}
