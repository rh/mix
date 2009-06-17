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

            if (files.Count == 0)
            {
                return 0;
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
            if (file.StartsWith(Environment.CurrentDirectory))
            {
                Context.FileName = file.Substring(Environment.CurrentDirectory.Length + 1);
            }
            else
            {
                Context.FileName = file;
            }
            Context.Encoding = GetFileEncoding(file);

            try
            {
                var document = new XmlDocument();
                document.Load(file);
                Context.Document = document;
            }
            catch (XmlException)
            {
                var message = String.Format("File '{0}' is not a valid XML file.", file);
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
                log.Error(e.Message, e);
                Context.Error.WriteLine(e.Message);
                return false;
            }
            catch (RequirementException e)
            {
                log.Error(e.Message, e);
                var message = String.Format("Required option '{0}' is missing.", e.Property.ToLower());
                Context.Error.WriteLine(message);
                if (e.Description.Length > 0)
                {
                    Context.Error.WriteLine("  " + e.Property.ToLower() + ": " + e.Description);
                }
                Context.Error.Write(Environment.NewLine);
                Context.Error.WriteLine("Type 'mix help {0}' for usage.", Task);
                return false;
            }
            catch (TaskExecutionException e)
            {
                log.Error(e.Message, e);
                Context.Error.WriteLine(e.Message);
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
                var settings = new XmlWriterSettings {Encoding = Context.Encoding};
                using (var writer = XmlWriter.Create(file, settings))
                {
                    Context.Document.WriteContentTo(writer);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
                Context.Error.WriteLine(e.Message);
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