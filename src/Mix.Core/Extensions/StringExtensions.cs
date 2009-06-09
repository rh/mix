using System;
using System.Text;

namespace Mix.Core.Extensions
{
    public static class StringExtensions
    {
        public static string Dasherize(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var result = new StringBuilder();
            result.Append(Char.ToLower(value[0]));
            for (var i = 1; i < value.Length; i++)
            {
                var c = value[i];
                if (Char.IsUpper(c))
                {
                    result.Append("-");
                }
                result.Append(Char.ToLower(c));
            }
            return result.ToString();
        }
    }
}