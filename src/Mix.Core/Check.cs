using System;
using System.Collections.Generic;

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

        public static void ArgumentIsNotNullOrEmpty(string[] value, string name)
        {
            ArgumentIsNotNull(value, name);
            if (value.Length == 0)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void ArgumentIsNotNullOrEmpty<T>(IList<T> list, string name)
        {
            ArgumentIsNotNull(list, name);
            if (list.Count == 0)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void ArgumentIsNotNullOrEmpty(IDictionary<string, string> dictionary, string name)
        {
            ArgumentIsNotNull(dictionary, name);
            if (dictionary.Count == 0)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}