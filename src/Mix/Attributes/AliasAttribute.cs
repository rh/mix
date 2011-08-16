using System;

namespace Mix.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class AliasAttribute : Attribute
    {
        /// <summary>
        /// Gets a comma-separated <see cref="String"/> of aliases.
        /// </summary>
        /// <remarks>
        /// The value returned is never <c>null</c>.
        /// </remarks>
        public string[] Aliases { get; private set; }

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
            Aliases = aliases.Replace(" ", "").Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
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
        /// All aliases defined on <paramref name="obj"/>, or an empty IList.
        /// </returns>
        public static string[] GetAliasesFrom(object obj)
        {
            if (IsDefinedOn(obj))
            {
                var attribute = (AliasAttribute) obj.GetType().GetCustomAttributes(typeof(AliasAttribute), false)[0];
                return attribute.Aliases;
            }
            return new string[] {};
        }
    }
}