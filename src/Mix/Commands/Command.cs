using System;

namespace Mix.Commands
{
    public abstract class Command
    {
        public virtual int Execute()
        {
            return 0;
        }

        private Context context = Context.Null;

        public virtual Context Context
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

        protected void Write(string value)
        {
            Context.Output.Write(value);
        }

        protected void Write(string format, params object[] args)
        {
            Context.Output.Write(format, args);
        }

        protected void WriteLine(string value)
        {
            Context.Output.WriteLine(value);
        }

        protected void WriteLine(string format, params object[] args)
        {
            Context.Output.WriteLine(format, args);
        }

        public override string ToString()
        {
            return GetType().Name.ToLower().Replace("command", "");
        }

        protected bool Equals(Command command)
        {
            if (command == null)
            {
                return false;
            }

            return GetType().Equals(command.GetType());
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return Equals(obj as Command);
        }

        public override int GetHashCode()
        {
            return GetType().FullName.GetHashCode();
        }
    }
}
