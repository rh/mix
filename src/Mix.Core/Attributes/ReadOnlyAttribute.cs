using System;

namespace Mix.Core.Attributes
{
    /// <summary>
    /// An attribute for tasks to declare that the task does not change any XML.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ReadOnlyAttribute : Attribute
    {
        public static bool IsDefinedOn(object obj)
        {
            return obj.GetType().IsDefined(typeof(ReadOnlyAttribute), false);
        }
    }
}