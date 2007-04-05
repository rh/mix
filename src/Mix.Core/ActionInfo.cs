using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Mix.Core.Attributes;

namespace Mix.Core
{
    public class ActionInfo : IActionInfo
    {
        private string name = String.Empty;
        private string description = "[no description]";
        private IArgumentInfo[] arguments = new IArgumentInfo[0] {};

        public ActionInfo()
        {
        }

        public string Name
        {
            get { return name; }
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

        public static IActionInfo[] All()
        {
            IList<Type> types = Actions();
            IActionInfo[] infos = new IActionInfo[types.Count];
            for (int i = 0; i < types.Count; i++)
            {
                object obj = Activator.CreateInstance(types[i]);
                infos[i] = For(obj);
            }
            return infos;
        }

        #region Reflection Helpers

        private static IList<Type> actionTypes;

        private static IList<Type> Actions()
        {
            if (actionTypes == null)
            {
                actionTypes = new List<Type>();
                // Explicitly load assembly Mix.Actions
                Assembly.Load("Mix.Actions");
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                AddActions(assemblies);
            }
            return actionTypes;
        }

        private static void AddActions(Assembly[] assemblies)
        {
            Check.ArgumentIsNotNull(assemblies, "assemblies");

            foreach (Assembly assembly in assemblies)
            {
                if (IsSystemAssembly(assembly))
                {
                    continue;
                }

                foreach (Type type in assembly.GetTypes())
                {
                    if (IsAction(type))
                    {
                        if (!actionTypes.Contains(type))
                        {
                            actionTypes.Add(type);
                        }
                    }
                }
            }
        }

        [DebuggerStepThrough]
        private static bool IsSystemAssembly(Assembly assembly)
        {
            string name = assembly.GetName().ToString().ToLower();
            return name.StartsWith("system") ||
                   name.StartsWith("microsoft") ||
                   name.StartsWith("vshost") ||
                   name == "mscorlib" ||
                   name.Contains("jetbrains") ||
                   name.Contains("resharper");
        }

        [DebuggerStepThrough]
        private static bool IsAction(Type type)
        {
            return typeof(Action).IsAssignableFrom(type) &&
                   !type.IsInterface &&
                   !type.IsAbstract;
        }

        #endregion
    }
}