using Mix.Core;

namespace Mix.Console.Commands
{
    internal sealed class UnknownCommand : Command
    {
        private readonly string name;

        public UnknownCommand(string name)
        {
            Check.ArgumentIsNotNullOrEmpty(name, "name");

            this.name = name;
        }

        public override int Execute()
        {
            WriteError("Unknown action:");
            WriteLine(" '{0}'", name);
            WriteLine("Type 'mix help' for usage.");
            return 1;
        }
    }
}
