using System;

namespace Mix.Core.Attributes
{
    /// <summary>
    /// An attribute for tasks to declare that selected nodes should be processed in reversed order.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ReversedAttribute : Attribute
    {
        public static bool IsDefinedOn(object obj)
        {
            return obj.GetType().IsDefined(typeof(ReversedAttribute), false);
        }
    }
}