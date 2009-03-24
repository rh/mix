using System.Collections;
using System.Xml;

/// <remarks>
/// See: http://www.tkachenko.com/blog/archives/000318.html
/// </remarks>
public class NamespaceCollectingXmlReader : XmlTextReader
{
    private readonly Hashtable namespaces = new Hashtable();

    public NamespaceCollectingXmlReader(string url)
        : base(url)
    {
    }

    public Hashtable CollectedNamespaces
    {
        get { return namespaces; }
    }

    public override bool Read()
    {
        var baseRead = base.Read();
        if (base.NodeType == XmlNodeType.Element && base.NamespaceURI != "")
        {
            if (!namespaces.ContainsKey(base.NamespaceURI))
            {
                namespaces.Add(base.NamespaceURI, "");
            }
        }
        return baseRead;
    }
}