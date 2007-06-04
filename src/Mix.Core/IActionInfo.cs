using System.Collections.Generic;

namespace Mix.Core
{
    public interface IActionInfo
    {
        IAction Instance { get; }
        string Name { get; }
        string Description { get; }
        IList<string> Aliases { get; }
        IArgumentInfo[] Arguments { get; }
    }
}