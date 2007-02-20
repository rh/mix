using System.Collections.Generic;

namespace Mix.Core
{
    public interface IContext : IDictionary<string, string>
    {
        string Xml { get; set; }
        string XPath { get; }
    }
}