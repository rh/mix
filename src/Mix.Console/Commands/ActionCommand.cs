using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using log4net;
using Mix.Core;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;

namespace Mix.Console.Commands
{
    public class ActionCommand : Command, IName
    {
        #region Variables

        private static readonly ILog log =
            LogManager.GetLogger(typeof(ActionCommand));

        private readonly Action action;
        private readonly string name;

        #endregion

        #region Constructor

        [DebuggerStepThrough]
        public ActionCommand(Action action)
        {
            this.action = action;
            name = GetNameFromAction();
        }

        #endregion

        #region Properties

        public virtual Action Action
        {
            [DebuggerStepThrough]
            get { return action; }
        }

        #endregion

        public override int Execute()
        {
            Debug.Assert(Context != null, "Context != null");

            if (ValidateFile() == false)
            {
                return 1;
            }

            string pattern = Context["file"];

            foreach (string file in Directory.GetFiles(".", pattern))
            {
                if (!ExecuteAction(file))
                {
                    break;
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
                action.Execute(Context);
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
                WriteLine("{1}Type 'mix help {0}' for usage.", Context.Action, Environment.NewLine);
                return false;
            }
            catch (ActionExecutionException e)
            {
                log.Error(e.Message, e);
                WriteLine(e.Message);
                return false;
            }

            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(Context.Xml);
                document.Save(file);
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
                WriteLine(e.Message);
                return false;
            }

            return true;
        }

        private bool ValidateFile()
        {
            if (!Context.ContainsKey("file"))
            {
                WriteLine("Required argument 'file' is missing.");
                return false;
            }

            if (String.IsNullOrEmpty(Context["file"]))
            {
                WriteLine("Required argument 'file' is not set correctly.");
                return false;
            }
            return true;
        }

        #region IName Members

        string IName.Name
        {
            [DebuggerStepThrough]
            get { return name; }
        }

        #endregion

        private string GetNameFromAction()
        {
            if (NameAttribute.IsDefinedOn(action))
            {
                return NameAttribute.GetNameFrom(action);
            }
            else
            {
                return action.GetType().Name.ToLower().Replace("action", "");
            }
        }
    }
}