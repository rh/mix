using System;
using System.Collections.Generic;

namespace Mix.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class AliasAttribute : Attribute
    {
        private readonly string alias;

        /// <summary>
        ///
        /// </summary>
        /// <param name="alias"></param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="alias"/> is <c>null</c>, or empty.
        /// </exception>
        public AliasAttribute(string alias)
        {
            Check.ArgumentIsNotNullOrEmpty(alias, "alias");

            this.alias = alias;
        }

        /// <summary>
        /// Gets a comma-separated <see cref="String"/> of aliases.
        /// </summary>
        /// <remarks>
        /// The value returned is never <c>null</c>.
        /// </remarks>
        public virtual string Aliases
        {
            get { return alias; }
        }

        /// <summary>
        /// Determines if an <see cref="AliasAttribute"/> is defined on
        /// <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsDefinedOn(object obj)
        {
            return obj.GetType().IsDefined(typeof(AliasAttribute), false);
        }

        /// <summary>
        /// Returns all aliases defined on <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// All aliases defined on <paramref name="obj"/>, or an empty
        /// <seealso cref="IList{T}"/>
        /// </returns>
        public static IList<string> GetAliasesFrom(object obj)
        {
            if (IsDefinedOn(obj))
            {
                AliasAttribute attribute =
                    (AliasAttribute)
                    obj.GetType().GetCustomAttributes(typeof(AliasAttribute), false)[0];
                string aliases = attribute.Aliases;
                IList<string> result = new List<string>();
                foreach (string alias in aliases.Split(','))
                {
                    if (alias.Trim().Length > 0)
                    {
                        result.Add(alias.Trim());
                    }
                }
                return result;
            }
            return new List<string>();
        }
    }
}