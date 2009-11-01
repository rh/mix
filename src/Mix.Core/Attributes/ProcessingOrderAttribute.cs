using System;

namespace Mix.Core.Attributes
{
    public enum ProcessingOrder
    {
        Normal,
        Reverse
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ProcessingOrderAttribute : Attribute
    {
        public ProcessingOrder ProcessingOrder { get; private set; }

        public ProcessingOrderAttribute()
        {
            ProcessingOrder = ProcessingOrder.Normal;
        }

        public ProcessingOrderAttribute(ProcessingOrder order)
        {
            ProcessingOrder = order;
        }

        public static bool IsDefinedOn(Type type)
        {
            return type.IsDefined(typeof(ProcessingOrderAttribute), false);
        }

        public static ProcessingOrder GetProcessingOrderFrom(object obj)
        {
            if (IsDefinedOn(obj.GetType()))
            {
                var attribute = (ProcessingOrderAttribute) obj.GetType().GetCustomAttributes(typeof(ProcessingOrderAttribute), false)[0];
                return attribute.ProcessingOrder;
            }
            return ProcessingOrder.Normal;
        }
    }
}