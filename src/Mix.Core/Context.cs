using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace Mix.Core
{
    [DebuggerStepThrough]
    public class Context : IContext
    {
        #region Instance variables

        private XmlDocument document;
        private XmlNamespaceManager namespaceManager;
        private string xpath;
        private Properties properties;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Context"/>.
        /// </summary>
        /// <param name="document">The <seealso cref="XmlDocument"/>
        /// this <see cref="IContext"/> applies to.</param>
        /// <param name="xpath">The XPath expression used to select nodes
        /// from the <paramref name="document"/>.</param>
        /// <param name="properties">
        /// The properties for this <see cref="Context"/>. May be <c>null</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="document"/> or
        /// <paramref name="xpath"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// The supplied <see cref="IDictionary"/> is copied.
        /// </remarks>
        /// <param name="namespaceManager"></param>
        public Context(XmlDocument document, string xpath, IDictionary<string, string> properties,
                       XmlNamespaceManager namespaceManager)
        {
            Check.ArgumentIsNotNull(document, "document");
            Check.ArgumentIsNotNullOrEmpty(xpath, "xpath");
            Check.ArgumentIsNotNull(namespaceManager, "namespaceManager");

            this.document = document;
            this.xpath = xpath;

            if (properties != null)
            {
                this.properties = new Properties(properties);
            }
            else
            {
                this.properties = new Properties();
            }

            this.namespaceManager = namespaceManager;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <seealso cref="XmlDocument"/>.
        /// </summary>
        public XmlDocument Document
        {
            [DebuggerStepThrough]
            get { return document; }
        }

        public XmlNamespaceManager NamespaceManager
        {
            [DebuggerStepThrough]
            get { return namespaceManager; }
        }

        /// <summary>
        /// Gets the XPath expression.
        /// </summary>
        public string XPath
        {
            [DebuggerStepThrough]
            get { return xpath; }
        }

        /// <summary>
        /// Gets a copy of the properties for this <see cref="Context"/>.
        /// </summary>
        public IDictionary<string, string> Properties
        {
            [DebuggerStepThrough]
            get { return new Properties(properties); }
        }

        #endregion
    }
}
