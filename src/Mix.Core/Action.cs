using System;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;

namespace Mix.Core
{
    /// <summary/>
    public abstract class Action : IAction
    {
        private void Initialize(IContext context)
        {
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
                return;
            }

            if (String.IsNullOrEmpty(context.XPath))
            {
                throw new RequirementException("'xpath' is required.", "xpath", "");
            }

            XmlDocument document = new XmlDocument();
            document.InnerXml = context.Xml;

            // Actions may need to recreate child nodes. If they do, these nodes
            // will not be selected. Processing all nodes in reverse order
            // solves this.
            XmlNodeList nodes = SelectNodes(context, document);
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
            }
            context.Xml = document.InnerXml;
        }

        private XmlNodeList SelectNodes(IContext context, XmlDocument document)
        {
            try
            {
                return document.SelectNodes(context.XPath);
            }
            catch (XPathException e)
            {
                string info = String.Empty;

                if (e.Message.StartsWith("Namespace prefix"))
                {
                    info = "\nNamespace-prefixes can be set like:" +
                           "\nnamespace:prefix:uri";
                }

                string message =
                    String.Format("'{0}' is not a valid XPath expression:{1}{2}{3}",
                                  context.XPath, Environment.NewLine, e.Message, info);
                throw new ActionExecutionException(message, e);
            }
        }

        /// <summary>
        /// Executes the action for the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <c>element</c> is <c>null</c>.
        /// </exception>
        /// <exception cref="ActionExecutionException">
        /// Thrown when <see cref="ExecuteCore(XmlElement)"/> throws an
        /// <see cref="Exception"/>.
        /// </exception>
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

        /// <summary>
        /// Executes the action for the specified attribute.
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <c>attribute</c> is <c>null</c>.
        /// </exception>
        /// <exception cref="ActionExecutionException">
        /// Thrown when <see cref="ExecuteCore(XmlAttribute)"/> throws an
        /// <see cref="Exception"/>.
        /// </exception>
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
                string message = String.Format("File '{0}' is not a valid XML document. {1}",
                                               context.FileName, e.Message);
                throw new XmlException(message, e);
            }
        }

        private void ValidateProperties()
        {
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (property.IsDefined(typeof(ArgumentAttribute), true) &&
                    property.IsDefined(typeof(RequiredAttribute), true))
                {
                    object value = property.GetValue(this, null);
                    if (value == null || value.ToString().Trim().Length == 0)
                    {
                        string message =
                            String.Format("'{0}' is required.",
                                          property.Name.ToLower());
                        string description = "";
                        if (property.IsDefined(typeof(DescriptionAttribute), true))
                        {
                            DescriptionAttribute attribute =
                                (DescriptionAttribute)
                                property.GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
                            description = attribute.Description;
                        }
                        throw new RequirementException(message, property.Name, description);
                    }
                }

                if (property.IsDefined(typeof(ArgumentAttribute), true) &&
                    property.IsDefined(typeof(XmlArgumentAttribute), true))
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
                                string message = String.Format("Argument '{0}' ('{1}') is not valid XML: {2}",
                                                               property.Name.ToLower(), xml, e.Message);
                                throw new XmlException(message, e);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// A method for derived classes to override if they do not wish to use
        /// <see cref="ExecuteCore(XmlElement)"/> or
        /// <see cref="ExecuteCore(XmlAttribute)"/>.
        /// </summary>
        /// <param name="context">
        /// An <see cref="IContext"/> instance containing all the necessary
        /// properties.
        /// </param>
        /// <returns><c>true</c> if the <see cref="IAction"/> is handled,
        /// <c>false</c> otherwise.</returns>
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

        public override string ToString()
        {
            return GetType().Name.ToLower().Replace("action", "");
        }
    }
}