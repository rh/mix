namespace Mix.Core
{
    public interface ITaskInfo
    {
        ITask Instance { get; }
        string Name { get; }
        string Description { get; }
        string[] Aliases { get; }
        IArgumentInfo[] Arguments { get; }
    }
}