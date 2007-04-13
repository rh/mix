namespace Mix.Core
{
    public interface IAction
    {
        void Execute(IContext context);
    }
}