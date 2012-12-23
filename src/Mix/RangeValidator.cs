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
        /// Checks if value is within the range specified on property.
        /// If property is not decorated with a RangeAttribute, a range of 1 up to int.MaxValue is used.
        /// </summary>
        /// <param name="property">
        /// The PropertyInfo which should be set with value.
        /// </param>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <param name="description">
        /// A description of the error if value is not valid, or an empty string.
        /// </param>
        /// <returns>
        /// true if value is valid; otherwise, false.
        /// </returns>
        public static bool Validate(PropertyInfo property, int value, out string description)
        {
            if (RangeAttribute.IsDefinedOn(property))
            {
                var attribute = (RangeAttribute) property.GetCustomAttributes(typeof(RangeAttribute), false)[0];

                if (value < attribute.MinValue)
                {
                    description = string.Format("Value should be greater than {0}.", attribute.MinValue);

                    return false;
                }

                if (value > attribute.MaxValue)
                {
                    description = string.Format("Value should be less than {0}.", attribute.MaxValue);

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

            description = string.Empty;

            return true;
        }
    }
}
