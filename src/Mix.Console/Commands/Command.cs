using System.Diagnostics;
using Mix.Core;

namespace Mix.Console.Commands
{
    [DebuggerStepThrough]
    internal abstract class Command
    {
        public virtual int Execute()
        {
            return 0;
        }

        private IContext context;

        public IContext Context
        {
            get { return context; }
            set { context = value; }
        }

        /// <summary>
        /// Shortcut for <seealso cref="System.Console.Write(string)"/>.
        /// </summary>
        /// <param name="value"></param>
        protected void Write(string value)
        {
            System.Console.Write(value);
        }

        /// <summary>
        /// Shortcut for <seealso cref="System.Console.Write(string, object[])"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        protected void Write(string format, params object[] args)
        {
            System.Console.Write(format, args);
        }

        /// <summary>
        /// Shortcut for <seealso cref="System.Console.WriteLine(string)"/>.
        /// </summary>
        /// <param name="value"></param>
        protected void WriteLine(string value)
        {
            System.Console.WriteLine(value);
        }

        /// <summary>
        /// Shortcut for <seealso cref="System.Console.WriteLine(string, object[])"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        protected void WriteLine(string format, params object[] args)
        {
            System.Console.WriteLine(format, args);
        }
    }
}