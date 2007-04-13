using System;

namespace Mix.Core
{
    public interface IActionInfo
    {
        Type Type { get; }
        string Name { get; }
        string Description { get; }
        IArgumentInfo[] Arguments { get; }
    }
}