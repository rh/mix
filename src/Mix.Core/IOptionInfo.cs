namespace Mix.Core
{
    public interface IOptionInfo
    {
        string Name { get; }
        string Description { get; }
        bool Required { get; }
    }
}