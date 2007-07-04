using System;
using System.Collections.Generic;
using System.Reflection;
using Mix.Core.Attributes;

namespace Mix.Core
{
    public class ActionInfo : IActionInfo
    {
        private IAction action;
        private string name = String.Empty;
        private string description = "[no description]";
        private string[] aliases = new string[] {};
        private IArgumentInfo[] arguments = new IArgumentInfo[0] {};

        public ActionInfo()
        {
        }

        public IAction Instance
        {
            get { return action; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }

        public string[] Aliases
        {
            get { return aliases; }
        }

        public IArgumentInfo[] Arguments
        {
            get { return arguments; }
        }

        public static IActionInfo For(object obj)
        {
            ActionInfo info = new ActionInfo();
            info.action = obj as IAction;
            info.name = obj.ToString();
            info.description = DescriptionAttribute.GetDescriptionFrom(obj, "[no description]");
            info.aliases = AliasAttribute.GetAliasesFrom(obj);
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

        private static void AddActions(IEnumerable<Assembly> assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                AddActions(assembly);
            }
        }

        private static void AddActions(Assembly assembly)
        {
            if (!IsSystemAssembly(assembly))
            {
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

        private static bool IsAction(Type type)
        {
            return typeof (IAction).IsAssignableFrom(type) &&
                   !type.IsInterface &&
                   !type.IsAbstract;
        }
    }
}