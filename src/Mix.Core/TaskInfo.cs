using System;
using System.Collections.Generic;
using System.Reflection;
using Mix.Core.Attributes;

namespace Mix.Core
{
    public class TaskInfo : ITaskInfo
    {
        private ITask task;
        private string name = String.Empty;
        private string description = "[no description]";
        private string[] aliases = new string[] {};
        private IOptionInfo[] options = new IOptionInfo[] {};

        public ITask Instance
        {
            get { return task; }
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

        public IOptionInfo[] Options
        {
            get { return options; }
        }

        public static ITaskInfo For(object obj)
        {
            var info = new TaskInfo {task = (obj as ITask), name = obj.ToString(), description = DescriptionAttribute.GetDescriptionFrom(obj, "[no description]"), aliases = AliasAttribute.GetAliasesFrom(obj), options = OptionInfo.For(obj)};
            return info;
        }

        public static ITaskInfo[] All()
        {
            var types = Tasks();
            var infos = new ITaskInfo[types.Count];
            for (var i = 0; i < types.Count; i++)
            {
                var obj = Activator.CreateInstance(types[i]);
                infos[i] = For(obj);
            }
            return infos;
        }

        private static IList<Type> taskTypes;

        private static IList<Type> Tasks()
        {
            if (taskTypes == null)
            {
                taskTypes = new List<Type>();
                // Explicitly load assembly Mix.Tasks
                Assembly.Load("Mix.Tasks");
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                AddTasks(assemblies);
            }
            return taskTypes;
        }

        private static void AddTasks(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                AddTasks(assembly);
            }
        }

        private static void AddTasks(Assembly assembly)
        {
            if (!IsSystemAssembly(assembly))
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (IsTask(type))
                    {
                        if (!taskTypes.Contains(type))
                        {
                            taskTypes.Add(type);
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

        private static bool IsTask(Type type)
        {
            return typeof(ITask).IsAssignableFrom(type) &&
                   !type.IsInterface &&
                   !type.IsAbstract;
        }
    }
}