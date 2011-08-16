using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;
using Mix.Attributes;
using Mix.Exceptions;
using Mix.Extensions;

namespace Mix
{
    public abstract class Task
    {
        public Context Context { get; private set; }

        private readonly Dictionary<PropertyInfo, string> propertiesToEvaluate = new Dictionary<PropertyInfo, string>();

        private void Initialize(Context context)
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
                        if (property.IsAnOption())
                        {
                            if (OptionAttribute.IsDefinedOn(property))
                            {
                                var attribute = (OptionAttribute) property.GetCustomAttributes(typeof(OptionAttribute), false)[0];
                                if (attribute.SupportsXPathTemplates)
                                {
                                    propertiesToEvaluate[property] = context[name];
                                }
                            }

                            property.SetValue(this, context[name], null);
                        }
                    }
                    else if (type == typeof(int))
                    {
                        int value;
                        if (Int32.TryParse(context[name], out value))
                        {
                            string description;
                            if (!RangeValidator.Validate(property, value, out description))
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
                else
                {
                    if (property.IsAnOption())
                    {
                        if (OptionAttribute.IsDefinedOn(property))
                        {
                            var attribute = (OptionAttribute) property.GetCustomAttributes(typeof(OptionAttribute), false)[0];
                            if (attribute.SupportsXPathTemplates)
                            {
                                var value = property.GetValue(this, null) ?? string.Empty;
                                context[name] = value.ToString();
                                propertiesToEvaluate[property] = context[name];
                            }
                        }
                    }
                }
            }
        }

        public void Execute(Context context)
        {
            Initialize(context);
            Validate();

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
            var nodes = context.Document.Select(context.XPath);

            BeforeExecute(nodes.Count);

            if (ReversedAttribute.IsDefinedOn(this))
            {
                for (var i = nodes.Count - 1; i >= 0; i--)
                {
                    Execute(nodes, i);
                }
            }
            else
            {
                for (var i = 0; i < nodes.Count; i++)
                {
                    Execute(nodes, i);
                }
            }

            AfterExecute();
        }

        public void BeforeAllExecute()
        {
            OnBeforeAllExecute();
        }

        public void AfterAllExecute()
        {
            OnAfterAllExecute();
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

        private void Execute(IList<XmlNode> nodes, int index)
        {
            var node = nodes[index];

            XPathTemplate.EvaluateProperties(node, this, propertiesToEvaluate);

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
                if (property.IsAnOption() && property.IsRequired())
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

        protected virtual void OnBeforeAllExecute()
        {
        }

        protected virtual void OnAfterAllExecute()
        {
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
        /// <returns><c>true</c> if the <see cref="Task"/> is handled, <c>false</c> otherwise.</returns>
        protected virtual bool ExecuteCore(Context context)
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

        public override string ToString()
        {
            return GetType().Name.Dasherize().ToLower().Replace("-task", "");
        }
    }
}