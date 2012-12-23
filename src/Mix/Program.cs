using System;
using Mix.Commands;

namespace Mix
{
    public static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            try
            {
                var factory = new CommandFactory();
                var command = factory.Create(args);

                return command.Execute();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("An unexpected error occurred.");
                Console.Error.WriteLine(e);

                return 1;
            }
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.Error.WriteLine("An unexpected error occurred.");
            Console.Error.WriteLine(e.ExceptionObject);
        }
    }
}
