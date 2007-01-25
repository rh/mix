using System.Xml;

namespace Mix.Core
{
    public interface IAction
    {
        void Execute(XmlElement element);
        void Execute(XmlAttribute attribute);
    }
}
