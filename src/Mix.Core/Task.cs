using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;
using Mix.Core.Extensions;

namespace Mix.Core
{
    public abstract class Task : ITask
    {
        public IContext Context { get; private set; }

        private void Initialize(IContext context)
        {
            Context = context;
            foreach (var property in GetType().GetProperties())
            {
                var name = property.Name.ToLower();
                if (context.ContainsKey(name))
                {
                    var type = property.PropertyType;
                    if (type == typeof(string))
                    {
                        property.SetValue(this, context[name], null);
                    }
                    else if (type == typeof(int))
                    {
                        int value;
                        if (Int32.TryParse(context[name], out value))
                        {
                            string description;
                            var validator = new RangeValidator();
                            if (!validator.Validate(property, value, out description))
                            {
                                var message = String.Format("'{0}' is not a valid value for {1}. {2}", context[name], name, description);
                                throw new TaskExecutionException(message);
                            }
                            property.SetValue(this, value, null);
                        }
                        else
                        {
                            var message = String.Format("'{0}' is not a valid value for {1}. An integer value is required.", context[name], name);
                            throw new TaskExecutionException(message);
                        }
                    }
                    else if (type == typeof(bool))
                    {
                        bool value;
                        if (String.IsNullOrEmpty(context[name]))
                        {
                            property.SetValue(this, true, null);
                        }
                        else if (Boolean.TryParse(context[name], out value))
                        {
                            property.SetValue(this, value, null);
                        }
                        else
                        {
                            var message = String.Format("'{0}' is not a valid value for {1}. A value of 'true' or 'false' is required.", context[name], name);
                            throw new TaskExecutionException(message);
                        }
                    }
                }
            }
        }

        public void Execute(IContext context)
        {
            Initialize(context);
            Validate();

            var manager = CreateNamespaceManager(Context.Document);

            if (ExecuteCore(context))
            {
                return; // The subclass has handled the task
            }

            // XPath is required for tasks that don't implement ExecuteCore(IContext)
            if (String.IsNullOrEmpty(context.XPath))
            {
                throw new RequirementException("'xpath' is required.", "xpath", "");
            }

            // Tasks may need to recreate child nodes. If they do, these nodes
            // will not be selected. Processing all nodes in reverse order solves this.
            var nodes = SelectNodes(context, manager);
            BeforeExecute(nodes.Count);

            var order = ProcessingOrderAttribute.GetProcessingOrderFrom(this);

            if (order == ProcessingOrder.Normal)
            {
                for (var i = 0; i < nodes.Count; i++)
                {
                    Execute(nodes, i);
                }
            }
            else
            {
                for (var i = nodes.Count - 1; i >= 0; i--)
                {
                    Execute(nodes, i);
                }
            }
            AfterExecute();
        }

        private static XmlNodeList SelectNodes(IContext context, XmlNamespaceManager manager)
        {
            try
            {
                return context.Document.SelectNodes(context.XPath, manager);
            }
            catch (XPathException e)
            {
                var message = String.Format("'{0}' is not a valid XPath expression.", context.XPath);
                throw new TaskExecutionException(message, e);
            }
        }

        private void BeforeExecute(int count)
        {
            try
            {
                OnBeforeExecute(count);
            }
            catch (RequirementException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new TaskExecutionException(e);
            }
        }

        private void AfterExecute()
        {
            try
            {
                OnAfterExecute();
            }
            catch (RequirementException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new TaskExecutionException(e);
            }
        }

        private void Execute(XmlNodeList nodes, int index)
        {
            var node = nodes[index];
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
            else if (node is XmlProcessingInstruction)
            {
                Execute(node as XmlProcessingInstruction);
            }
            else if (node is XmlDocument)
            {
                Execute(node as XmlDocument);
            }

            // The 'generic' method is always executed, so subclasses need only to
            // implement ExecuteCore(XmlNode) for generic behaviour.
            Execute(node);
        }

        private void Execute(XmlDocument document)
        {
            try
            {
                ExecuteCore(document);
            }
            catch (RequirementException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new TaskExecutionException(e);
            }
        }

        private void Execute(XmlElement element)
        {
            try
            {
                ExecuteCore(element);
            }
            catch (RequirementException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new TaskExecutionException(e);
            }
        }

        private void Execute(XmlAttribute attribute)
        {
            try
            {
                ExecuteCore(attribute);
            }
            catch (RequirementException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new TaskExecutionException(e);
            }
        }

        private void Execute(XmlText text)
        {
            try
            {
                ExecuteCore(text);
            }
            catch (RequirementException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new TaskExecutionException(e);
            }
        }

        private void Execute(XmlCDataSection section)
        {
            try
            {
                ExecuteCore(section);
            }
            catch (RequirementException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new TaskExecutionException(e);
            }
        }

        private void Execute(XmlComment comment)
        {
            try
            {
                ExecuteCore(comment);
            }
            catch (RequirementException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new TaskExecutionException(e);
            }
        }

        private void Execute(XmlProcessingInstruction instruction)
        {
            try
            {
                ExecuteCore(instruction);
            }
            catch (RequirementException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new TaskExecutionException(e);
            }
        }

        private void Execute(XmlNode node)
        {
            try
            {
                ExecuteCore(node);
            }
            catch (RequirementException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new TaskExecutionException(e);
            }
        }

        private void Validate()
        {
            foreach (var property in GetType().GetProperties())
            {
                if ((OptionAttribute.IsDefinedOn(property) || XmlOptionAttribute.IsDefinedOn(property) || RegexOptionAttribute.IsDefinedOn(property)) &&
                    RequiredAttribute.IsDefinedOn(property))
                {
                    var value = property.GetValue(this, null);
                    if (value == null || value.ToString().Length == 0)
                    {
                        var message = String.Format("'{0}' is required.", property.Name.ToLower());
                        var description = "";
                        if (property.IsDefined(typeof(DescriptionAttribute), false))
                        {
                            var attribute = (DescriptionAttribute) property.GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
                            description = attribute.Description;
                        }
                        throw new RequirementException(message, property.Name, description);
                    }
                }

                if (XmlOptionAttribute.IsDefinedOn(property))
                {
                    var value = property.GetValue(this, null);
                    if (value != null && value.ToString().Trim().Length > 0)
                    {
                        var xml = value as string;
                        if (xml != null)
                        {
                            try
                            {
                                var document = new XmlDocument();
                                // Create a root node, because the XML is just a fragment.
                                document.LoadXml(String.Format("<root>{0}</root>", xml));
                            }
                            catch (XmlException e)
                            {
                                var message = String.Format("Option '{0}' ('{1}') is not valid XML: {2}", property.Name.ToLower(), xml, e.Message);
                                throw new XmlException(message, e);
                            }
                        }
                    }
                }

                if (RegexOptionAttribute.IsDefinedOn(property))
                {
                    var value = property.GetValue(this, null);
                    if (value != null && value.ToString().Trim().Length > 0)
                    {
                        var pattern = value as string;
                        if (pattern != null)
                        {
                            try
                            {
                                new Regex(pattern);
                            }
                            catch (ArgumentException e)
                            {
                                var message = String.Format("Option '{0}' is not a valid regular expression: {1}", property.Name.ToLower(), e.Message);
                                throw new ArgumentException(message, e);
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
        /// <returns><c>true</c> if the <see cref="ITask"/> is handled, <c>false</c> otherwise.</returns>
        protected virtual bool ExecuteCore(IContext context)
        {
            return false;
        }

        protected virtual void ExecuteCore(XmlDocument document)
        {
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

        protected virtual void ExecuteCore(XmlProcessingInstruction instruction)
        {
        }

        protected virtual void ExecuteCore(XmlNode node)
        {
        }

        /// <summary>
        /// Creates a <see cref="XmlNamespaceManager"/> for <paramref name="document"/>.
        /// Namespaces declared in the document node are automatically added.
        /// The default namespace is given the prefix 'default'.
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private static XmlNamespaceManager CreateNamespaceManager(XmlDocument document)
        {
            var manager = new XmlNamespaceManager(document.NameTable);
            foreach (XmlNode node in document.SelectNodes("//node()"))
            {
                if (node is XmlElement)
                {
                    var element = node as XmlElement;
                    foreach (XmlAttribute attribute in element.Attributes)
                    {
                        if (attribute.Name == "xmlns")
                        {
                            // The first default namespace wins
                            // (since using multiple default namespaces in a single file is not considered a good practice)
                            if (!manager.HasNamespace("default"))
                            {
                                manager.AddNamespace("default", attribute.Value);
                            }
                        }
                        if (attribute.Prefix == "xmlns")
                        {
                            manager.AddNamespace(attribute.LocalName, attribute.Value);
                        }
                    }
                }
            }
            return manager;
        }

        public override string ToString()
        {
            return GetType().Name.Dasherize().ToLower().Replace("-task", "");
        }
    }
}