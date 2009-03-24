using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using log4net;
using Mix.Console.Exceptions;
using Mix.Core;
using Mix.Core.Exceptions;

namespace Mix.Console.Commands
{
    public class TaskCommand : Command
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TaskCommand));
        private readonly ITask task;

        public TaskCommand(ITask task)
        {
            this.task = task;
        }

        public override IContext Context
        {
            set
            {
                base.Context = value;
                base.Context.Task = Task.ToString();
            }
        }

        public virtual ITask Task
        {
            get { return task; }
        }

        public override int Execute()
        {
            if (String.IsNullOrEmpty(Context["file"]))
            {
                Context.Output.WriteLine("No files have been selected.");
                return 1;
            }

            IList<string> files;

            try
            {
                var paths = new PathExpander();
                files = paths.Expand(Environment.CurrentDirectory, Context["file"]);
            }
            catch (InvalidPathException e)
            {
                Context.Output.WriteLine(e.Message);
                return 1;
            }

            if (files.Count == 0)
            {
                Context.Output.WriteLine("No files have been selected.");
                return 1;
            }

            foreach (var file in files)
            {
                if (!ExecuteTask(file))
                {
                    return 1;
                }
            }

            return 0;
        }

        private bool ExecuteTask(string file)
        {
            Context.FileName = file;
            Context.Encoding = GetFileEncoding(file);

            try
            {
                Context.Xml = File.ReadAllText(file);
            }
            catch (ArgumentNullException)
            {
                var message = String.Format("File '{0}' is empty.", file);
                WriteLine(message);
                return false;
            }

            try
            {
                Task.Execute(Context);
            }
            catch (XmlException e)
            {
                log.Error(e.Message, e);
                WriteLine(e.Message);
                return false;
            }
            catch (RequirementException e)
            {
                log.Error(e.Message, e);
                var message = String.Format("Required argument '{0}' is missing.", e.Property.ToLower());
                WriteLine(message);
                if (e.Description.Length > 0)
                {
                    WriteLine("  " + e.Property.ToLower() + ": " + e.Description);
                }
                Write(Environment.NewLine);
                WriteLine("Type 'mix help {0}' for usage.", Context.Task);
                return false;
            }
            catch (TaskExecutionException e)
            {
                log.Error(e.Message, e);
                WriteLine(e.Message);
                return false;
            }

            if (Task is IReadOnly)
            {
                return true;
            }
            return Save(file);
        }

        private bool Save(string file)
        {
            try
            {
                var document = new XmlDocument();
                document.LoadXml(Context.Xml);
                var settings = new XmlWriterSettings {Encoding = Context.Encoding};
                using (var writer = XmlWriter.Create(file, settings))
                {
                    document.WriteContentTo(writer);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
                log.Error(Context.Xml);
                WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Reads the encoding of <paramref name="file"/> from the byte order mark.
        /// If no byte order mark is found, <see cref="Encoding.Default"/> is assumed.
        /// </summary>
        private static Encoding GetFileEncoding(string file)
        {
            using (var reader = new StreamReader(file, true))
            {
                reader.Read();
                var encoding = reader.CurrentEncoding;
                if (encoding != null)
                {
                    return encoding;
                }
            }

            using (var reader = XmlReader.Create(file))
            {
                if (reader.Read() && reader.NodeType == XmlNodeType.XmlDeclaration)
                {
                    var name = reader.GetAttribute("encoding");
                    if (name != null)
                    {
                        try
                        {
                            return Encoding.GetEncoding(name);
                        }
                        catch (ArgumentException)
                        {
                            // An ArgumentException is raised by Encoding.GetEncoding() if no valid code page name is given
                            // or the code page is not supported by the underlying platform
                            return Encoding.UTF8;
                        }
                    }
                }
            }

            return Encoding.UTF8;
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