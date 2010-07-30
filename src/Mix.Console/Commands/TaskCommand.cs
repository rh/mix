using System;
using System.Collections.Generic;
using System.Xml;
using Mix.Console.Exceptions;
using Mix.Core;
using Mix.Core.Attributes;
using Mix.Core.Exceptions;

namespace Mix.Console.Commands
{
    public class TaskCommand : Command
    {
        private readonly ITask task;

        public TaskCommand(ITask task)
        {
            this.task = task;
        }

        public virtual ITask Task
        {
            get { return task; }
        }

        public override int Execute()
        {
            if (String.IsNullOrEmpty(Context["file"]))
            {
                return 0;
            }

            IList<string> files;

            try
            {
                var paths = new PathExpander();
                var recursive = Context.ContainsKey("recursive");
                files = paths.Expand(Environment.CurrentDirectory, Context["file"], recursive);
            }
            catch (InvalidPathException e)
            {
                Context.Error.WriteLine(e.Message);
                return 1;
            }

			Task.BeforeAllExecute();

            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (!ExecuteTask(file))
                    {
                        return 1;
                    }
                }
            }

			Task.AfterAllExecute();

            return 0;
        }

        private bool ExecuteTask(string file)
        {
            if (file.StartsWith(Environment.CurrentDirectory))
            {
                Context.FileName = file.Substring(Environment.CurrentDirectory.Length + 1);
            }
            else
            {
                Context.FileName = file;
            }

            try
            {
                var document = new XmlDocument();
                document.Load(file);
                Context.Document = document;
            }
            catch (XmlException e)
            {
                var message = String.Format("File '{0}' is not a valid XML file:{1}{2}", file, Environment.NewLine, e.Message);
                Context.Error.WriteLine(message);
                return false;
            }

            try
            {
                Task.Execute(Context);
            }
            catch (ArgumentException e)
            {
                Context.Error.WriteLine(e.Message);
                return false;
            }
            catch (XmlException e)
            {
                Context.Error.WriteLine(e.Message);
                return false;
            }
            catch (RequirementException e)
            {
                var message = String.Format("Required option '{0}' is not set.", e.Property.ToLower());
                Context.Error.WriteLine(message);
                if (e.Description.Length > 0)
                {
                    Context.Error.WriteLine("  " + e.Property.ToLower() + ": " + e.Description);
                }
                Context.Error.Write(Environment.NewLine);
                Context.Error.WriteLine("Type 'mix help {0}' for usage.", Task);
                return false;
            }
            catch (XPathTemplateException e)
            {
                var message = String.Format("XPath template '{0}' evaluates to an empty value for at least one of the selected nodes, but option '{1}' is required.", e.Value, e.Property.ToLower());
                Context.Error.WriteLine(message);
            }
            catch (TaskExecutionException e)
            {
                Context.Error.WriteLine(e.Message);
                return false;
            }

            if (ReadOnlyAttribute.IsDefinedOn(Task))
            {
                return true;
            }
            return Save(file);
        }

        private bool Save(string file)
        {
            try
            {
				Context.Document.Save(file);
            }
            catch (Exception e)
            {
                Context.Error.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return Task.ToString();
        }

        protected bool Equals(TaskCommand taskCommand)
        {
            if (taskCommand == null)
            {
                return false;
            }
            if (!base.Equals(taskCommand))
            {
                return false;
            }
            return Equals(task, taskCommand.task);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return Equals(obj as TaskCommand);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + 29 * task.GetHashCode();
        }
    }
}