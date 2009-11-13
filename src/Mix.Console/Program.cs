using System;
using Mix.Console.Commands;

namespace Mix.Console
{
    public class Program
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
                System.Console.Error.WriteLine("An unexpected error occurred.");
                System.Console.Error.WriteLine(e);
                return 1;
            }
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            System.Console.Error.WriteLine("An unexpected error occurred.");
            System.Console.Error.WriteLine(e.ExceptionObject);
        }
    }
}