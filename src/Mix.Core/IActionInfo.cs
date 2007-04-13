namespace Mix.Core
{
    public interface IActionInfo
    {
        IAction Instance { get; }
        string Name { get; }
        string Description { get; }
        IArgumentInfo[] Arguments { get; }
    }
}