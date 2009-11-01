using System;
using log4net;
using log4net.Config;
using Mix.Console.Commands;

namespace Mix.Console
{
    public class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            XmlConfigurator.Configure();

            try
            {
                var factory = new CommandFactory();
                var command = factory.Create(args);
                return command.Execute();
            }
            catch (Exception e)
            {
                const string message = "An exception was caught. Mix exits.";
                LogManager.GetLogger(typeof(Program)).Error(message, e);
                System.Console.Error.WriteLine(message);
                System.Console.Error.WriteLine(e);
                return 1;
            }
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var logger = LogManager.GetLogger(typeof(Program));
            logger.Error("An unhandled exception was caught.", e.ExceptionObject as Exception);
        }
    }
}