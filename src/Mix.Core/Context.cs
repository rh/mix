using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Mix.Core
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerStepThrough]
    public class Context : Dictionary<string, string>, IContext
    {
        #region Instance variables

        private string filename = String.Empty;
        private string action = String.Empty;
        private TextWriter output = TextWriter.Null;
        private TextWriter error = TextWriter.Null;
        private string xml = String.Empty;
        private string xpath = String.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public Context()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        public Context(string xml)
            : this(xml, String.Empty)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xpath"></param>
        public Context(string xml, string xpath)
            : this(xml, xpath, TextWriter.Null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xpath"></param>
        /// <param name="output">A <seealso cref="TextWriter"/> that will 
        /// represent the standard output stream.</param>
        public Context(string xml, string xpath, TextWriter output)
            : this(xml, xpath, output, TextWriter.Null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xpath"></param>
        /// <param name="output">A <seealso cref="TextWriter"/> that will 
        /// represent the standard output stream.</param>
        /// <param name="error">A <seealso cref="TextWriter"/> that will 
        /// represent the standard error stream.</param>
        public Context(string xml, string xpath, TextWriter output, TextWriter error)
        {
            Check.ArgumentIsNotNullOrEmpty(xml, "xml");
            Check.ArgumentIsNotNull(output, "output");
            Check.ArgumentIsNotNull(error, "error");

            this.xml = xml;
            this.xpath = xpath ?? String.Empty;
            this.output = output;
            this.error = error;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public Context(IContext context)
        {
            filename = context.FileName;
            action = context.Action;
            output = context.Output;
            error = context.Error;
            xml = context.Xml;
            xpath = context.XPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="properties"></param>
        public Context(IDictionary<string, string> properties)
        {
            foreach (KeyValuePair<string, string> pair in properties)
            {
                Add(pair.Key, pair.Value);
            }

            if (ContainsKey("action"))
            {
                action = this["action"] ?? String.Empty;
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
        /// The name of the file this <see cref="IContext"/> applies to.
        /// </summary>
        public virtual string FileName
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

        /// <summary>
        /// The name of the <see cref="Action"/> this <see cref="IContext"/>
        /// applies to.
        /// </summary>
        public virtual string Action
        {
            get { return action; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }
                action = value;
            }
        }

        /// <summary>
        /// A <seealso cref="TextWriter"/> that represents the standard output
        /// stream.
        /// </summary>
        public virtual TextWriter Output
        {
            get { return output; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                output = value;
            }
        }

        /// <summary>
        /// A <seealso cref="TextWriter"/> that represents the standard error
        /// stream.
        /// </summary>
        public virtual TextWriter Error
        {
            get { return error; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                error = value;
            }
        }

        /// <summary>
        /// Gets or sets the XML.
        /// </summary>
        public virtual string Xml
        {
            get { return xml; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value");
                }
                xml = value;
            }
        }

        /// <summary>
        /// Gets the XPath expression.
        /// </summary>
        public virtual string XPath
        {
            get { return xpath; }
        }

        #endregion

        #region NullContext

        public static IContext Null
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

        #endregion
    }
}