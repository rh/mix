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

            XmlNodeList nodes = SelectNodes(context, document);

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
                    String.Format("'{0}' is not a valid XPath expression: {1}{2}",
                                  context.XPath, e.Message, info);
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
            Check.ArgumentIsNotNull(element, "element");

            Validate();

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
            Check.ArgumentIsNotNull(attribute, "attribute");

            Validate();

            try
            {
                ExecuteCore(attribute);
            }
            catch (Exception e)
            {
                throw new ActionExecutionException(e);
            }
        }

        private void Execute(XmlComment comment)
        {
            Check.ArgumentIsNotNull(comment, "comment");

            Validate();

            try
            {
                ExecuteCore(comment);
            }
            catch (Exception e)
            {
                throw new ActionExecutionException(e);
            }
        }

        private void Validate()
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
            }
        }

        #region Methods that could be overridden by a derived class

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

        protected virtual void ExecuteCore(XmlComment comment)
        {
        }

        #endregion

        public override string ToString()
        {
            return GetType().Name.ToLower().Replace("action", "");
        }
    }
}