using System;
using System.Collections.Generic;

namespace Mix.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class AliasAttribute : Attribute
    {
        private readonly string[] aliases;

        /// <summary>
        ///
        /// </summary>
        /// <param name="aliases">
        /// An alias for the class that has been decorated with this attribute,
        /// or a comma-separated-list of aliases. 
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="aliases"/> is <c>null</c>, or empty.
        /// </exception>
        public AliasAttribute(string aliases)
        {
            Check.ArgumentIsNotNullOrEmpty(aliases, "aliases");
            this.aliases = aliases.Replace(" ", "").Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Gets a comma-separated <see cref="String"/> of aliases.
        /// </summary>
        /// <remarks>
        /// The value returned is never <c>null</c>.
        /// </remarks>
        public virtual string[] Aliases
        {
            get { return aliases; }
        }

        /// <summary>
        /// Determines if an <see cref="AliasAttribute"/> is defined on
        /// <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsDefinedOn(object obj)
        {
            return obj.GetType().IsDefined(typeof (AliasAttribute), false);
        }

        /// <summary>
        /// Returns all aliases defined on <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        /// All aliases defined on <paramref name="obj"/>, or an empty
        /// <seealso cref="IList{T}"/>
        /// </returns>
        public static string[] GetAliasesFrom(object obj)
        {
            if (IsDefinedOn(obj))
            {
                AliasAttribute attribute = (AliasAttribute) obj.GetType().GetCustomAttributes(typeof (AliasAttribute), false)[0];
                return attribute.Aliases;
            }
            return new string[] {};
        }
    }
}