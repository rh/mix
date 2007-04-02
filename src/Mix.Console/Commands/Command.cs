using System;
using System.Diagnostics;
using Mix.Core;

namespace Mix.Console.Commands
{
    [DebuggerStepThrough]
    public abstract class Command
    {
        public virtual int Execute()
        {
            return 0;
        }

        private IContext context = Core.Context.Null;

        public virtual IContext Context
        {
            get { return context; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                context = value;
            }
        }

        /// <summary>
        /// Writes <paramref name="value"/> to the standard output stream.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to write.</param>
        protected void Write(string value)
        {
            Context.Output.Write(value);
        }

        /// <summary>
        /// Writes <paramref name="value"/>, followed by the current line 
        /// terminator, to the standard output stream.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to write.</param>
        protected void WriteLine(string value)
        {
            Context.Output.WriteLine(value);
        }

        /// <summary>
        /// Writes out a formatted string, followed by the current line 
        /// terminator, to the standard output stream, using the same semantics
        /// as <see cref="string.Format(string, object)"/>.
        /// </summary>
        /// <param name="format">A <see cref="string"/> containing zero or more
        /// format items.</param>
        /// <param name="args">An <see cref="object"/> array containing zero or
        /// more objects to format.</param>
        protected void WriteLine(string format, params object[] args)
        {
            Context.Output.WriteLine(format, args);
        }

        public override string ToString()
        {
            return GetType().Name.ToLower().Replace("command", "");
        }
    }
}