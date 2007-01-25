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
        private string filename;
        private string xpath;

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

        [Argument, Required]
        [Alias("file")]
        public string Filename
        {
            [DebuggerStepThrough]
            get { return filename; }
            [DebuggerStepThrough]
            set { filename = value; }
        }

        [Argument, Required]
        [Alias("select")]
        public string XPath
        {
            [DebuggerStepThrough]
            get { return xpath; }
            [DebuggerStepThrough]
            set { xpath = value; }
        }

        public Action Action
        {
            [DebuggerStepThrough]
            get { return action; }
        }

        #endregion

        public override int Execute()
        {
            if (Properties == null)
            {
                throw new InvalidOperationException("Property 'Properties' is not set.");
            }

            if (!Properties.ContainsKey("file"))
            {
                WriteLineError("Required argument 'file' is missing.");
                return 1;
            }

            if (!Properties.ContainsKey("xpath") && !Properties.ContainsKey("select"))
            {
                WriteLineError("Required argument 'xpath' (or 'select') is missing.");
                return 1;
            }

            string file = Properties["file"];

            if (String.IsNullOrEmpty(file))
            {
                WriteLineError("Required argument 'file' is not set correctly.");
                return 1;
            }

            if (!File.Exists(file))
            {
                WriteError("File does not exist: ");
                WriteLine(file);
                return 1;
            }

            XmlDocument document = new XmlDocument();
            document.Load(file);
            XmlNamespaceManager namespaceManager =
                new XmlNamespaceManager(document.NameTable);

            if (Properties.ContainsKey("namespace"))
            {
                int position = Properties["namespace"].IndexOf(":");
                if (position == -1)
                {
                    WriteLineError("Namespace not set correctly; use namespace:prefix:uri.");
                    return 1;
                }

                string prefix = Properties["namespace"].Split(':')[0];
                string uri = Properties["namespace"].Substring(position + 1);
                namespaceManager.AddNamespace(prefix, uri);
            }

            string query;
            if (Properties.ContainsKey("xpath"))
            {
                query = Properties["xpath"];
            }
            else
            {
                query = Properties["select"];
            }

            if (String.IsNullOrEmpty(query))
            {
                WriteLine("Required argument 'xpath' (or 'select') is not set correctly.");
                return 1;
            }

            IContext context = new Context(document, query, Properties, namespaceManager);
            Executor executor = new Executor(context, action);
            try
            {
                executor.Execute();
            }
            catch (RequiredValueMissingException e)
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

                WriteLineError(e.Message);
            }
            finally
            {
                try
                {
                    document.Save(file);
                }
                catch (Exception e)
                {
                    log.Error(e.Message, e);

                    WriteLineError(e.Message);
                }
            }

            return 0;
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
