using System;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;

namespace Mix.Core
{
    public abstract class Action : IAction
    {
        private IContext context;

        public IContext Context
        {
            get { return context; }
        }

        private void Initialize(IContext context)
        {
            this.context = context;
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                string name = property.Name.ToLower();
                if (context.ContainsKey(name))
                {
                    property.SetValue(this, context[name], null);
                }
            }
        }

        public void Execute(IContext context)
        {
            Initialize(context);
            Validate(context);

            if (ExecuteCore(context))
            {
                return; // The subclass has handled the action
            }

            // XPath is required for actions that don't implement ExecuteCore(IContext)
            if (String.IsNullOrEmpty(context.XPath))
            {
                throw new RequirementException("'xpath' is required.", "xpath", "");
            }

            XmlDocument document = new XmlDocument();
            document.InnerXml = context.Xml;
            XmlNamespaceManager manager = XmlHelper.CreateNamespaceManager(document);

            // Actions may need to recreate child nodes. If they do, these nodes
            // will not be selected. Processing all nodes in reverse order solves this.
            XmlNodeList nodes = SelectNodes(context, document, manager);
            BeforeExecute(nodes.Count);
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                XmlNode node = nodes[i];
                if (node is XmlElement)
                {
                    Execute(node as XmlElement);
                }
                else if (node is XmlAttribute)
                {
                    Execute(node as XmlAttribute);
                }
                else if (node is XmlText)
                {
                    Execute(node as XmlText);
                }
                else if (node is XmlCDataSection)
                {
                    Execute(node as XmlCDataSection);
                }
                else if (node is XmlComment)
                {
                    Execute(node as XmlComment);
                }

                // The 'generic' method is always executed, so subclasses need only to
                // implement ExecuteCore(XmlNode) for generic behaviour.
                Execute(node);
            }
            AfterExecute();
            context.Xml = document.InnerXml;
        }

        private XmlNodeList SelectNodes(IContext context, XmlDocument document, XmlNamespaceManager manager)
        {
            try
            {
                return document.SelectNodes(context.XPath, manager);
            }
            catch (XPathException e)
            {
                string message = String.Format("'{0}' is not a valid XPath expression:{1}{2}", context.XPath, Environment.NewLine, e.Message);
                throw new ActionExecutionException(message, e);
            }
        }

        private void BeforeExecute(int count)
        {
            try
            {
                OnBeforeExecute(count);
            }
            catch (Exception e)
            {
                throw new ActionExecutionException(e);
            }
        }

        private void AfterExecute()
        {
            try
            {
                OnAfterExecute();
            }
            catch (Exception e)
            {
                throw new ActionExecutionException(e);
            }
        }

        private void Execute(XmlElement element)
        {
            try
            {
                ExecuteCore(element);
            }
            catch (Exception e)
            {
                throw new ActionExecutionException(e);
            }
        }

        private void Execute(XmlAttribute attribute)
        {
            try
            {
                ExecuteCore(attribute);
            }
            catch (Exception e)
            {
                throw new ActionExecutionException(e);
            }
        }

        private void Execute(XmlText text)
        {
            try
            {
                ExecuteCore(text);
            }
            catch (Exception e)
            {
                throw new ActionExecutionException(e);
            }
        }

        private void Execute(XmlCDataSection section)
        {
            try
            {
                ExecuteCore(section);
            }
            catch (Exception e)
            {
                throw new ActionExecutionException(e);
            }
        }

        private void Execute(XmlComment comment)
        {
            try
            {
                ExecuteCore(comment);
            }
            catch (Exception e)
            {
                throw new ActionExecutionException(e);
            }
        }

        private void Execute(XmlNode node)
        {
            try
            {
                ExecuteCore(node);
            }
            catch (Exception e)
            {
                throw new ActionExecutionException(e);
            }
        }

        private void Validate(IContext context)
        {
            ValidateXml(context);
            ValidateProperties();
        }

        private void ValidateXml(IContext context)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.LoadXml(context.Xml);
            }
            catch (XmlException e)
            {
                string message = String.Format("File '{0}' is not a valid XML document. {1}", context.FileName, e.Message);
                throw new XmlException(message, e);
            }
        }

        private void ValidateProperties()
        {
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (property.IsDefined(typeof(ArgumentAttribute), false) &&
                    property.IsDefined(typeof(RequiredAttribute), false))
                {
                    object value = property.GetValue(this, null);
                    if (value == null || value.ToString().Trim().Length == 0)
                    {
                        string message = String.Format("'{0}' is required.", property.Name.ToLower());
                        string description = "";
                        if (property.IsDefined(typeof(DescriptionAttribute), false))
                        {
                            DescriptionAttribute attribute = (DescriptionAttribute) property.GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
                            description = attribute.Description;
                        }
                        throw new RequirementException(message, property.Name, description);
                    }
                }

                if (property.IsDefined(typeof(ArgumentAttribute), false) &&
                    property.IsDefined(typeof(XmlArgumentAttribute), false))
                {
                    object value = property.GetValue(this, null);
                    if (value != null && value.ToString().Trim().Length > 0)
                    {
                        string xml = value as string;
                        if (xml != null)
                        {
                            try
                            {
                                XmlDocument document = new XmlDocument();
                                // Create a root node, because the XML is just a fragment.
                                document.LoadXml(String.Format("<root>{0}</root>", xml));
                            }
                            catch (XmlException e)
                            {
                                string message = String.Format("Argument '{0}' ('{1}') is not valid XML: {2}", property.Name.ToLower(), xml, e.Message);
                                throw new XmlException(message, e);
                            }
                        }
                    }
                }
            }
        }

        /// <param name="count">The number of selected nodes.</param>
        protected virtual void OnBeforeExecute(int count)
        {
        }

        protected virtual void OnAfterExecute()
        {
        }

        /// <summary>
        /// A method for derived classes to override if they do not wish to use one of
        /// the other ExecuteCore(*) methods.
        /// </summary>
        /// <param name="context">
        /// An <see cref="IContext"/> instance containing all the necessary properties.
        /// </param>
        /// <returns><c>true</c> if the <see cref="IAction"/> is handled, <c>false</c> otherwise.</returns>
        protected virtual bool ExecuteCore(IContext context)
        {
            return false;
        }

        protected virtual void ExecuteCore(XmlElement element)
        {
        }

        protected virtual void ExecuteCore(XmlAttribute attribute)
        {
        }

        protected virtual void ExecuteCore(XmlText text)
        {
        }

        protected virtual void ExecuteCore(XmlCDataSection section)
        {
        }

        protected virtual void ExecuteCore(XmlComment comment)
        {
        }

        protected virtual void ExecuteCore(XmlNode node)
        {
        }

        public override string ToString()
        {
            return GetType().Name.ToLower().Replace("action", "");
        }
    }
}