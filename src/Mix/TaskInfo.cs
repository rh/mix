using System;
using System.Collections.Generic;
using System.Reflection;
using Mix.Attributes;

namespace Mix
{
    public class TaskInfo
    {
        private Task task;
        private string name = String.Empty;
        private string description = "[no description]";
        private string[] aliases = new string[] {};
        private OptionInfo[] options = new OptionInfo[] {};

        public Task Instance
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

        public OptionInfo[] Options
        {
            get { return options; }
        }

        public static TaskInfo For(object obj)
        {
            var info = new TaskInfo
                           {
                               task = (obj as Task),
                               name = obj.ToString(),
                               description = DescriptionAttribute.GetDescriptionFrom(obj, "[no description]"),
                               aliases = AliasAttribute.GetAliasesFrom(obj),
                               options = OptionInfo.For(obj)
                           };
            return info;
        }

        public static TaskInfo[] All()
        {
            var types = Tasks();
            var infos = new TaskInfo[types.Count];
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
                foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (typeof(Task).IsAssignableFrom(type) && !type.IsAbstract)
                    {
                        taskTypes.Add(type);
                    }
                }
            }

            return taskTypes;
        }
    }
}