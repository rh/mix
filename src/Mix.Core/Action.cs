using System;
using System.Diagnostics;
using System.Reflection;
using System.Xml;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;

namespace Mix.Core
{
    /// <summary>
    /// Determines whether the <see cref="Action"/> acts on the element/
    /// attribute itself, or on its value.
    /// </summary>
    public enum ActionType
    {
        Property,
        Value
    }

    /// <summary/>
    public abstract class Action
    {
        private ActionType actionType = ActionType.Value;

        #region Properties

        /// <summary>
        /// Gets or sets the type of the action.
        /// Defaults to <c>ActionType.Value</c>.
        /// </summary>
        /// <value>The type of the action.</value>
        public ActionType ActionType
        {
            [DebuggerStepThrough]
            get { return actionType; }
            [DebuggerStepThrough]
            set { actionType = value; }
        }

        #endregion

        #region IAction implementation

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
        public void Execute(XmlElement element)
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
        public void Execute(XmlAttribute attribute)
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

        #endregion

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
                            String.Format("Required argument '{0}' is missing.",
                                          property.Name.ToLower());
                        string description = "";
                        if (property.IsDefined(typeof(DescriptionAttribute), true))
                        {
                            DescriptionAttribute attribute =
                                (DescriptionAttribute)
                                property.GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
                            description = attribute.Description;
                        }
                        throw new RequiredValueMissingException(message, property.Name, description);
                    }
                }
            }
        }

        #region Methods that could be overridden by a derived class

        protected virtual void ExecuteCore(XmlElement element)
        {
        }

        protected virtual void ExecuteCore(XmlAttribute attribute)
        {
        }

        #endregion
    }
}
