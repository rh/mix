using System;
using System.Reflection;

namespace Mix.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class RangeAttribute : Attribute
    {
        private readonly int minValue = 1;
        private readonly int maxValue = Int32.MaxValue;

        public RangeAttribute(int maxValue)
        {
            this.maxValue = maxValue;
        }

        public RangeAttribute(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public int MinValue
        {
            get { return minValue; }
        }
        public int MaxValue
        {
            get { return maxValue; }
        }

        public static bool IsDefinedOn(PropertyInfo property)
        {
            return property.IsDefined(typeof(RangeAttribute), false);
        }
    }
}