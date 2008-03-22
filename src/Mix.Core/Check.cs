using System;

namespace Mix.Core
{
    public static class Check
    {
        public static void ArgumentIsNotNull(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void ArgumentIsNotNullOrEmpty(string value, string name)
        {
            ArgumentIsNotNull(value, name);
            if (value.Length == 0)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}