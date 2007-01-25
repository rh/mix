using System;
using System.Collections.Generic;
using System.Diagnostics;
using log4net;
using Mix.Core;

namespace Mix.Console.Commands
{
    [DebuggerStepThrough]
    internal abstract class Command
    {
        private static ILog log =
            LogManager.GetLogger(typeof(Command));

        public virtual int Execute()
        {
            return 0;
        }

        private IDictionary<string, string> properties;

        public IDictionary<string, string> Properties
        {
            get { return properties; }
            set
            {
                Check.ArgumentIsNotNullOrEmpty(value, "value");
                properties = value;
            }
        }

        /// <summary>
        /// Shortcut for <seealso cref="System.Console.Write(string)"/>.
        /// </summary>
        /// <param name="value"></param>
        protected void Write(string value)
        {
            Check.ArgumentIsNotNullOrEmpty(value, "value");

            System.Console.Write(value);
        }

        /// <summary>
        /// Shortcut for <seealso cref="System.Console.Write(string, object[])"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        protected void Write(string format, params object[] args)
        {
            Check.ArgumentIsNotNullOrEmpty(format, "format");
            Check.ArgumentIsNotNull(args, "args");

            System.Console.Write(format, args);
        }

        /// <summary>
        /// Shortcut for <seealso cref="System.Console.WriteLine(string)"/>.
        /// </summary>
        /// <param name="value"></param>
        protected void WriteLine(string value)
        {
            Check.ArgumentIsNotNullOrEmpty(value, "value");

            System.Console.WriteLine(value);
        }

        /// <summary>
        /// Shortcut for <seealso cref="System.Console.WriteLine(string, object[])"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        protected void WriteLine(string format, params object[] args)
        {
            Check.ArgumentIsNotNullOrEmpty(format, "format");
            Check.ArgumentIsNotNull(args, "args");

            System.Console.WriteLine(format, args);
        }

        protected void WriteError(string value)
        {
            Check.ArgumentIsNotNullOrEmpty(value, "value");

            WriteColor(value, ConsoleColor.DarkRed);
        }

        protected void WriteLineError(string value)
        {
            Check.ArgumentIsNotNullOrEmpty(value, "value");

            WriteLineColor(value, ConsoleColor.DarkRed);
        }

        /// <summary>
        /// Writes <paramref name="value"/> and <paramref name="exception"/>
        /// to the <seealso cref="System.Console"/> in two different colors.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="exception"></param>
        protected void WriteLineError(string value, string exception)
        {
            Check.ArgumentIsNotNullOrEmpty(value, "value");
            Check.ArgumentIsNotNullOrEmpty(exception, "exception");

            WriteLineColor(value, ConsoleColor.DarkRed);
            WriteLineColor(exception, ConsoleColor.DarkYellow);
        }

        protected void WriteColor(string value, ConsoleColor color)
        {
            Check.ArgumentIsNotNullOrEmpty(value, "value");
            Check.ArgumentIsNotNull(color, "color");

            ConsoleColor foregroundColor = System.Console.ForegroundColor;
            try
            {
                System.Console.ForegroundColor = color;
                System.Console.Write(value);
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
            }
            finally
            {
                System.Console.ForegroundColor = foregroundColor;
            }
        }

        protected void WriteLineColor(string value, ConsoleColor color)
        {
            Check.ArgumentIsNotNullOrEmpty(value, "value");
            Check.ArgumentIsNotNull(color, "color");

            WriteColor(String.Format("{0}{1}", value, Environment.NewLine), color);
        }

        protected void WriteLineColor(string format, ConsoleColor color, params object[] args)
        {
            Check.ArgumentIsNotNullOrEmpty(format, "format");
            Check.ArgumentIsNotNull(color, "color");
            Check.ArgumentIsNotNull(args, "args");

            WriteLineColor(String.Format(format, args), color);
        }
    }
}
