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

            var sb = new StringBuilder();
            sb.Append(Char.ToLower(value[0]));
            for (var i = 1; i < value.Length; i++)
            {
                var c = value[i];
                if (Char.IsUpper(c))
                {
                    sb.Append("-");
                }
                sb.Append(Char.ToLower(c));
            }
            return sb.ToString();
        }
    }
}