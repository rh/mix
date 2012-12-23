using System;

namespace Mix.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class AliasAttribute : Attribute
    {
        public string[] Aliases { get; private set; }

        public AliasAttribute(string aliases)
        {
            Aliases = aliases.Replace(" ", "").Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool IsDefinedOn(object obj)
        {
            return obj.GetType().IsDefined(typeof(AliasAttribute), false);
        }

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
