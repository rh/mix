using System.Collections.Generic;
using System.Diagnostics;

namespace Mix.Core
{
    /// <summary>
    /// A convenient implementation of
    /// <seealso cref="IDictionary{TKey,TValue}"/>, used for ease-of-use,
    /// readability and an easy way to copy
    /// <seealso cref="IDictionary{TKey,TValue}"/>'s.
    /// </summary>
    [DebuggerStepThrough]
    public sealed class Properties : Dictionary<string, string>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Properties"/>.
        /// </summary>
        public Properties()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Properties"/>.
        /// </summary>
        /// <param name="properties">The properties which the new
        /// <see cref="Properties"/> should be initialized with.</param>
        /// <remarks>
        /// The supplied <see cref="IDictionary{TKey,TValue}"/> is copied.
        /// <br/>
        /// This constructor does not use the base constructor because it does
        /// not accept <c>null</c> for its <c>dictionary</c> parameter, see
        /// <seealso cref="Dictionary{TKey,TValue}"/>.
        /// </remarks>
        public Properties(IDictionary<string, string> properties)
        {
            if (properties != null)
            {
                foreach (KeyValuePair<string, string> pair in properties)
                {
                    Add(pair.Key, pair.Value);
                }
            }
        }
    }
}
