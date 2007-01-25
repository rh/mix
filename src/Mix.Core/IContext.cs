using System.Collections.Generic;
using System.Xml;

namespace Mix.Core
{
    public interface IContext
    {
        XmlDocument Document { get; }
        XmlNamespaceManager NamespaceManager { get; }
        string XPath { get; }
        IDictionary<string, string> Properties { get; }
    }
}
