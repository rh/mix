using System;
using log4net;
using log4net.Config;
using Mix.Console.Commands;

namespace Mix.Console
{
    internal class Program
    {
        private static readonly ILog log =
            LogManager.GetLogger(typeof(Program));

        private static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(OnUnhandledException);
            XmlConfigurator.Configure();
            log.Info("Mix started.");

            try
            {
                Command command = CommandFactory.Create(args);
                int code = command.Execute();
                log.Info(String.Format("Mix exits. Return code is {0}.", code));
                return code;
            }
            catch (Exception e)
            {
                string message = "An exception was caught. Mix exits.";
                log.Error(message, e);
                ConsoleColor color = System.Console.ForegroundColor;
                System.Console.ForegroundColor = ConsoleColor.DarkRed;
                System.Console.WriteLine(message);
                System.Console.ForegroundColor = ConsoleColor.DarkYellow;
                System.Console.WriteLine(e);
                System.Console.ForegroundColor = color;
                return 1;
            }
        }

        #region How to do this in the future?

//        public static void Main(string[] args)
//        {
//            System.Console.WriteLine("Mix, version 0.1.0");
//            System.Console.WriteLine("Type 'h' for help, 'q' to quit...\n");
//            string command;
//
//            do
//            {
//                Prompt();
//                command = System.Console.ReadLine().Trim();
//            } while (command != "q");
//        }
//
//        public static void Prompt()
//        {
//            System.Console.Write("mix> ");
//        }

        #endregion

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Error("An unhandled exception was caught.", e.ExceptionObject as Exception);
        }
    }
}
