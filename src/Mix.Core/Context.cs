using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mix.Core
{
    [DebuggerStepThrough]
    public class Context : Dictionary<string, string>, IContext
    {
        #region Instance variables

        private string xml = String.Empty;
        private string xpath = String.Empty;

        #endregion

        #region Constructors

        public Context()
        {
        }

        public Context(string xml)
            : this(xml, String.Empty)
        {
        }

        public Context(string xml, string xpath)
        {
            this.xml = xml;
            this.xpath = xpath;
        }

        public Context(IDictionary<string, string> properties)
        {
            foreach (KeyValuePair<string, string> pair in properties)
            {
                Add(pair.Key, pair.Value);
            }

            if (ContainsKey("xpath"))
            {
                xpath = this["xpath"] ?? String.Empty;
            }

            if (ContainsKey("xml"))
            {
                xml = this["xml"] ?? String.Empty;
            }
        }

        #endregion

        #region IContext Members

        /// <summary>
        /// Gets or sets the XML.
        /// </summary>
        public string Xml
        {
            get { return xml; }
            set { xml = value; }
        }

        /// <summary>
        /// Gets the XPath expression.
        /// </summary>
        public string XPath
        {
            get { return xpath; }
        }

        #endregion
    }
}