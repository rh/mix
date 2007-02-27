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
    internal class ActionCommand : Command, IName
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

        public Action Action
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

            Context.Xml = File.ReadAllText(Context["file"]);

            try
            {
                action.Execute(Context);
            }
            catch (RequirementException e)
            {
                log.Error(e.Message, e);

                WriteLine(e.Message);
                if (e.Description.Length > 0)
                {
                    WriteLine(e.Property + ": " + e.Description);
                }
            }
            catch (ActionExecutionException e)
            {
                log.Error(e.Message, e);
                WriteLine(e.Message);
            }
            finally
            {
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(Context.Xml);
                    document.Save(Context["file"]);
                }
                catch (Exception e)
                {
                    log.Error(e.Message, e);
                    WriteLine(e.Message);
                }
            }

            return 0;
        }

        private bool ValidateFile()
        {
            if (!Context.ContainsKey("file"))
            {
                WriteLine("Required argument 'file' is missing.");
                return false;
            }

            string file = Context["file"];

            if (String.IsNullOrEmpty(file))
            {
                WriteLine("Required argument 'file' is not set correctly.");
                return false;
            }

            if (!File.Exists(file))
            {
                WriteLine("File does not exist: " + file);
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