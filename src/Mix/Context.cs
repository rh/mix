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
            Debug = TextWriter.Null;
            Error = TextWriter.Null;
            Output = TextWriter.Null;
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

        public TextWriter Output { get; set; }

        public TextWriter Error { get; set; }

        public TextWriter Debug { get; set; }

        public XmlDocument Document { get; set; }

        public XmlNamespaceManager NamespaceManager { get; set; }

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
