namespace Mix.Console.Commands
{
    internal sealed class InfoCommand : Command
    {
        public override int Execute()
        {
            WriteLine("Type 'mix help' for usage.");
            return 0;
        }
    }
}
