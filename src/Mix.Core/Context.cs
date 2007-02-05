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

        private XmlNamespaceManager namespaceManager;
        private string xml = String.Empty;
        private string xpath = String.Empty;
        private Properties properties;
        private string fileName = String.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="Context"/>.
        /// </summary>
        /// <param name="xml">The this <see cref="IContext"/> applies to.</param>
        /// <param name="xpath">The XPath expression used to select nodes
        /// from the <paramref name="xml"/>.</param>
        /// <param name="properties">
        /// The properties for this <see cref="Context"/>. May be <c>null</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="xpath"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// The supplied <see cref="IDictionary"/> is copied.
        /// </remarks>
        /// <param name="namespaceManager"></param>
        public Context(string xml, string xpath, IDictionary<string, string> properties,
                       XmlNamespaceManager namespaceManager)
        {
            Check.ArgumentIsNotNullOrEmpty(xpath, "xpath");
            Check.ArgumentIsNotNull(namespaceManager, "namespaceManager");

            this.xml = xml;
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

        public Context(string xml, string xpath)
        {
            this.xml = xml;
            this.xpath = xpath;
        }

        #endregion

        #region Indexer

        public string this[string key]
        {
            get { return properties[key]; }
        }

        #endregion

        #region Properties

        public XmlNamespaceManager NamespaceManager
        {
            [DebuggerStepThrough]
            get { return namespaceManager; }
        }

        #region IContext Members

        public string FileName
        {
            get { return fileName; }
        }

        #endregion

        /// <summary>
        /// Gets the XML.
        /// </summary>
        public string Xml
        {
            [DebuggerStepThrough]
            get { return xml; }
            [DebuggerStepThrough]
            set { xml = value; }
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