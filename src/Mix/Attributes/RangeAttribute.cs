using System;
using System.Reflection;

namespace Mix.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class RangeAttribute : Attribute
    {
        public int MinValue { get; private set; }
        public int MaxValue { get; private set; }

        public RangeAttribute(int maxValue)
        {
            MinValue = 1;
            MaxValue = maxValue;
        }

        public RangeAttribute(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public static bool IsDefinedOn(PropertyInfo property)
        {
            return property.IsDefined(typeof(RangeAttribute), false);
        }
    }
}