using System;
using log4net;
using log4net.Config;
using Mix.Console.Commands;

namespace Mix.Console
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(OnUnhandledException);
            XmlConfigurator.Configure();

            try
            {
                Command command = CommandFactory.Create(args);
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
            LogManager.GetLogger(typeof(Program)).Error("An unhandled exception was caught.", e.ExceptionObject as Exception);
        }
    }
}