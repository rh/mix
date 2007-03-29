using System.Collections.Generic;
using System.IO;

namespace Mix.Core
{
    public interface IContext : IDictionary<string, string>
    {
        /// <summary>
        /// The name of the file this <see cref="IContext"/> applies to.
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// The name of the <see cref="Action"/> this <see cref="IContext"/>
        /// applies to.
        /// </summary>
        string Action { get; set; }

        /// <summary>
        /// A <seealso cref="TextWriter"/> that represents the standard output
        /// stream.
        /// </summary>
        TextWriter Output { get; }

        /// <summary>
        /// A <seealso cref="TextWriter"/> that represents the standard error
        /// stream.
        /// </summary>
        TextWriter Error { get; }

        /// <summary>
        /// The XML this <see cref="IContext"/> applies to.
        /// </summary>
        string Xml { get; set; }

        /// <summary>
        /// Gets the XPath expression.
        /// May be <c>null</c>.
        /// </summary>
        string XPath { get; }
    }
}