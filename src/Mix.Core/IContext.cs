using System.Collections.Generic;
using System.Xml;

namespace Mix.Core
{
    public interface IContext
    {
        XmlNamespaceManager NamespaceManager { get; }
        string FileName { get; }
        string Xml { get; set; }
        string XPath { get; }
        IDictionary<string, string> Properties { get; }
    }
}