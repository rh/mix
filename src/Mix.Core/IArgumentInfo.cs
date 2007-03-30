namespace Mix.Core
{
    public interface IArgumentInfo
    {
        string Name { get; }
        string Description { get; }
        bool Required { get; }
    }
}