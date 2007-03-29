namespace Mix.Console.Commands
{
    public class InfoCommand : Command
    {
        public override int Execute()
        {
            WriteLine("Type 'mix help' for usage.");
            return 0;
        }
    }
}