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
        private readonly ProcessingOrder order = ProcessingOrder.Normal;

        public ProcessingOrderAttribute()
        {
        }

        public ProcessingOrderAttribute(ProcessingOrder order)
        {
            this.order = order;
        }

        public ProcessingOrder ProcessingOrder
        {
            get { return order; }
        }

        public static bool IsDefinedOn(Type type)
        {
            return type.IsDefined(typeof(ProcessingOrderAttribute), false);
        }

        public static ProcessingOrder GetProcessingOrderFrom(object obj)
        {
            if (IsDefinedOn(obj.GetType()))
            {
                ProcessingOrderAttribute attribute = (ProcessingOrderAttribute) obj.GetType().GetCustomAttributes(typeof(ProcessingOrderAttribute), false)[0];
                return attribute.ProcessingOrder;
            }
            return ProcessingOrder.Normal;
        }
    }
}