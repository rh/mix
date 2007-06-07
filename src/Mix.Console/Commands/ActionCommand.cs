using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using log4net;
using Mix.Core;
using Mix.Core.Exceptions;

namespace Mix.Console.Commands
{
    public class ActionCommand : Command
    {
        private static readonly ILog log =
            LogManager.GetLogger(typeof(ActionCommand));

        private readonly IAction action;

        public ActionCommand(IAction action)
        {
            this.action = action;
        }

        public override IContext Context
        {
            set
            {
                base.Context = value;
                base.Context.Action = Action.ToString();
            }
        }

        public virtual IAction Action
        {
            get { return action; }
        }

        public override int Execute()
        {
            Debug.Assert(Context != null, "Context != null");

            string pattern = Context["file"];
            string[] files;

            try
            {
                files = Directory.GetFiles(".", pattern);
            }
            catch (ArgumentException e)
            {
                log.Error("The pattern is not valid.", e);
                Context.Error.WriteLine("'{0}' is not a valid filename or pattern.", pattern);
                return 1;
            }

            if (files == null || files.Length == 0)
            {
                Context.Output.WriteLine("No files have been selected.");
                return 1;
            }

            foreach (string file in files)
            {
                if (!ExecuteAction(file))
                {
                    return 1;
                }
            }

            return 0;
        }

        private bool ExecuteAction(string file)
        {
            Context.FileName = file;
            Context.Xml = File.ReadAllText(file);

            try
            {
                Action.Execute(Context);
            }
            catch (RequirementException e)
            {
                log.Error(e.Message, e);
                string message =
                    String.Format("Required argument '{0}' is missing.",
                                  e.Property.ToLower());
                WriteLine(message);
                if (e.Description.Length > 0)
                {
                    WriteLine("  " + e.Property.ToLower() + ": " + e.Description);
                }
                Write(Environment.NewLine);
                WriteLine("Type 'mix help {0}' for usage.", Context.Action);
                return false;
            }
            catch (ActionExecutionException e)
            {
                log.Error(e.Message, e);
                WriteLine(e.Message);
                return false;
            }

            if (Action is IReadOnly)
            {
                return true;
            }
            else
            {
                return Save(file);
            }
        }

        private bool Save(string file)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(Context.Xml);
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(file, settings))
                {
                    document.WriteContentTo(writer);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
                log.Error(Context.Xml);
                WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return Action.ToString();
        }
    }
}