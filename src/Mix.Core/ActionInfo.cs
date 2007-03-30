using System;
using Mix.Core.Attributes;

namespace Mix.Core
{
    public class ActionInfo : IActionInfo
    {
        private string name = String.Empty;
        private string[] aliases = new string[0] {};
        private string description = "[no description]";
        private IArgumentInfo[] arguments = new IArgumentInfo[0] {};

        public ActionInfo()
        {
        }

        public string Name
        {
            get { return name; }
        }

        public string[] Aliases
        {
            get { return aliases; }
        }

        public string Description
        {
            get { return description; }
        }

        public IArgumentInfo[] Arguments
        {
            get { return arguments; }
        }

        public static IActionInfo For(object obj)
        {
            ActionInfo info = new ActionInfo();
            info.name = obj.ToString();
            info.description =
                DescriptionAttribute.GetDescriptionFrom(obj, "[no description]");
            info.arguments = ArgumentInfo.For(obj);
            return info;
        }
    }
}