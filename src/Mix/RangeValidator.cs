using System;
using System.Reflection;
using Mix.Attributes;

namespace Mix
{
    /// <summary>
    /// Validates integers to check if they are within a range specified by <see cref="RangeAttribute"/>.
    /// </summary>
    public class RangeValidator
    {
        /// <summary>
        /// Checks if <paramref name="value"/> is within the range specified on <paramref name="property"/>.
        /// If <paramref name="property"/> is not decorated with a <see cref="RangeAttribute"/>, a range
        /// of 1 up to <see cref="int.MaxValue"/> is used.
        /// </summary>
        /// <param name="property">
        /// The <see cref="PropertyInfo"/> which should be set with <paramref name="value"/>.
        /// </param>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <param name="description">
        /// A description of the error if <paramref name="value"/> is not valid, or an empty <see cref="string"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="value"/> is valid; otherwise, <c>false</c>.
        /// </returns>
        /// <seealso cref="RangeAttribute"/>
        public bool Validate(PropertyInfo property, int value, out string description)
        {
            if (RangeAttribute.IsDefinedOn(property))
            {
                var attribute = (RangeAttribute) property.GetCustomAttributes(typeof(RangeAttribute), false)[0];

                if (value < attribute.MinValue)
                {
                    description = String.Format("Value should be greater than {0}.", attribute.MinValue);
                    return false;
                }

                if (value > attribute.MaxValue)
                {
                    description = String.Format("Value should be less than {0}.", attribute.MaxValue);
                    return false;
                }
            }
            else
            {
                // No RangeAttribute set; a default range of 1..Int32.MaxValue (the default of RangeAttribute) is used.
                if (value < 1)
                {
                    description = "Value should be greater than 0.";
                    return false;
                }
            }

            description = String.Empty;
            return true;
        }
    }
}