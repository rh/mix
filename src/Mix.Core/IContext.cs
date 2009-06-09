using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Mix.Core
{
    public interface IContext : IDictionary<string, string>
    {
        /// <summary>The name of the file this <see cref="IContext"/> applies to.</summary>
        string FileName { get; set; }

        Encoding Encoding { get; set; }

        XmlDocument Document { get; set; }

        XmlNamespaceManager NamespaceManager { get; set; }

        /// <summary>Gets the XPath expression. May be <c>null</c>.</summary>
        string XPath { get; set; }

        /// <summary>The name of the <see cref="Task"/> this <see cref="IContext"/> applies to.</summary>
        string Task { get; set; }

        /// <summary>A <see cref="TextWriter"/> that represents the standard output stream.</summary>
        TextWriter Output { get; }

        /// <summary>A <see cref="TextWriter"/> that represents the standard error stream.</summary>
        TextWriter Error { get; }
    }
}