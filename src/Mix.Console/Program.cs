using System;
using log4net;
using log4net.Config;
using Mix.Console.Commands;

namespace Mix.Console
{
    public class Program
    {
        private static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(OnUnhandledException);
            XmlConfigurator.Configure();

            try
            {
                CommandFactory factory = new CommandFactory();
                Command command = factory.Create(args);
                return command.Execute();
            }
            catch (Exception e)
            {
                string message = "An exception was caught. Mix exits.";
                LogManager.GetLogger(typeof(Program)).Error(message, e);
                System.Console.WriteLine(message);
                System.Console.WriteLine(e);
                return 1;
            }
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ILog logger = LogManager.GetLogger(typeof(Program));
            logger.Error("An unhandled exception was caught.", e.ExceptionObject as Exception);
        }
    }
}