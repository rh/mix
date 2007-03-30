namespace Mix.Core
{
    public interface IActionInfo
    {
        string Name { get; }
        string[] Aliases { get; }
        string Description { get; }
        IArgumentInfo[] Arguments { get; }
    }
}