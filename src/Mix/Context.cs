using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Mix
{
    public class Context : Dictionary<string, string>
    {
        private string filename = String.Empty;

        public Context()
        {
            XPath = String.Empty;
            Error = TextWriter.Null;
            Output = TextWriter.Null;
            this["file"] = "*.xml";
        }

        public Context(IEnumerable<KeyValuePair<string, string>> properties)
            : this()
        {
            foreach (var pair in properties)
            {
                this[pair.Key] = pair.Value;
            }

            if (ContainsKey("xpath"))
            {
                XPath = this["xpath"] ?? String.Empty;
            }
        }

        /// <summary>The name of the file this context applies to.</summary>
        public string FileName
        {
            get { return filename; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }
                filename = value;
                if (filename.StartsWith(@".\"))
                {
                    filename = filename.Remove(0, 2);
                }
            }
        }

        /// <summary>A <seealso cref="TextWriter"/> that represents the standard output stream.</summary>
        public TextWriter Output { get; set; }

        /// <summary>A <seealso cref="TextWriter"/> that represents the standard error stream.</summary>
        public TextWriter Error { get; set; }

        public XmlDocument Document { get; set; }

        public XmlNamespaceManager NamespaceManager { get; set; }

        /// <summary>Gets the XPath expression.</summary>
        public string XPath { get; set; }

        public static Context Null
        {
            get { return new NullContext(); }
        }

        private class NullContext : Context
        {
            public override bool Equals(object obj)
            {
                return (obj as NullContext != null);
            }

            public override int GetHashCode()
            {
                return 0;
            }
        }
    }
}