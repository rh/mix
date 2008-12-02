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
        private IArgumentInfo[] arguments = new IArgumentInfo[] {};

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
            var info = new ActionInfo {action = (obj as IAction), name = obj.ToString(), description = DescriptionAttribute.GetDescriptionFrom(obj, "[no description]"), aliases = AliasAttribute.GetAliasesFrom(obj), arguments = ArgumentInfo.For(obj)};
            return info;
        }

        public static IActionInfo[] All()
        {
            var types = Actions();
            var infos = new IActionInfo[types.Count];
            for (var i = 0; i < types.Count; i++)
            {
                var obj = Activator.CreateInstance(types[i]);
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
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                AddActions(assemblies);
            }
            return actionTypes;
        }

        private static void AddActions(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                AddActions(assembly);
            }
        }

        private static void AddActions(Assembly assembly)
        {
            if (!IsSystemAssembly(assembly))
            {
                foreach (var type in assembly.GetTypes())
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
            var name = assembly.GetName().ToString().ToLower();
            return name.StartsWith("system") ||
                   name.StartsWith("microsoft") ||
                   name.StartsWith("vshost") ||
                   name == "mscorlib" ||
                   name.Contains("jetbrains") ||
                   name.Contains("resharper");
        }

        private static bool IsAction(Type type)
        {
            return typeof(IAction).IsAssignableFrom(type) &&
                   !type.IsInterface &&
                   !type.IsAbstract;
        }
    }
}