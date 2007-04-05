namespace Mix.Core
{
    public interface IActionInfo
    {
        string Name { get; }
        string Description { get; }
        IArgumentInfo[] Arguments { get; }
    }
}