namespace Mix.Console.Commands
{
    public class UnknownCommand : Command
    {
        private readonly string name;

        public UnknownCommand(string name)
        {
            this.name = name;
        }

        public override int Execute()
        {
            WriteLine("Unknown command: '{0}'", name);
            WriteLine("Type 'mix help' for usage.");
            return 1;
        }
    }
}