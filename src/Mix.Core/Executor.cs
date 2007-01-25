using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;
using Mix.Core.Exceptions;

namespace Mix.Core
{
    public class Executor
    {
        private IContext context;
        private readonly IList<Action> actions;

        [DebuggerStepThrough]
        public Executor(IContext context, Action action)
        {
            Check.ArgumentIsNotNull(context, "context");
            Check.ArgumentIsNotNull(action, "action");

            this.context = context;
            actions = new List<Action>();
            actions.Add(action);
        }

        [DebuggerStepThrough]
        public Executor(IContext context, IList<Action> actions)
        {
            Check.ArgumentIsNotNull(context, "context");
            Check.ArgumentIsNotNullOrEmpty(actions, "actions");

            this.context = context;
            this.actions = actions;
        }

        public string Xml
        {
            [DebuggerStepThrough]
            get { return context.Document.InnerXml; }
        }

        private void Initialize()
        {
            foreach (Action action in actions)
            {
                foreach (PropertyInfo property in action.GetType().GetProperties())
                {
                    string name = property.Name.ToLower();
                    if (context.Properties.ContainsKey(name))
                    {
                        property.SetValue(action, context.Properties[name], null);
                    }
                }
            }
        }

        public void Execute()
        {
            Initialize();

            XmlNodeList nodes;

            try
            {
                nodes = context.Document.SelectNodes(context.XPath, context.NamespaceManager);
            }
            catch (XPathException e)
            {
                string info = String.Empty;

                if (e.Message.StartsWith("Namespace prefix"))
                {
                    info = "\nYou can set the namespace-prefix with the 'namespace' argument:" +
                           "\nnamespace:prefix:uri";
                }
                // TODO: Namespace prefix '' is not defined.
                string message =
                    String.Format("'{0}' is not a valid XPath expression: {1}{2}",
                                  context.XPath, e.Message, info);
                throw new ActionExecutionException(message, e);
            }

            foreach (XmlNode node in nodes)
            {
                if (node is XmlElement)
                {
                    Execute(node as XmlElement);
                }
                else if (node is XmlAttribute)
                {
                    Execute(node as XmlAttribute);
                }
            }
        }

        [DebuggerStepThrough]
        private void Execute(XmlElement element)
        {
            foreach (Action action in actions)
            {
                action.Execute(element);
            }
        }

        [DebuggerStepThrough]
        private void Execute(XmlAttribute attribute)
        {
            foreach (Action action in actions)
            {
                action.Execute(attribute);
            }
        }
    }
}
