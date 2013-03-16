using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Mix.Attributes;

namespace Mix.Commands
{
    [Description("Shows the version of this application.")]
    public class VersionCommand : Command
    {
        public override int Execute()
        {
            WriteVersion();
            WriteCompilationDate();
            WriteCopyright();

            return 0;
        }

        private void WriteVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var name = assembly.GetName();
            var version = name.Version;
            WriteLine("Mix, version {0}", version);
        }

        private void WriteCompilationDate()
        {
            FileInfo fileInfo;

            try
            {
                fileInfo = new FileInfo(Assembly.GetExecutingAssembly().Location);
            }
            catch (Exception)
            {
                return;
            }

            if (fileInfo.Exists)
            {
                DateTime date;

                try
                {
                    date = fileInfo.LastWriteTime;
                }
                catch (Exception)
                {
                    return;
                }

                var ci = new CultureInfo("en-US");
                WriteLine("  compiled {0}", date.ToString("MMMM %d yyyy, HH:mm:ss", ci));
                Write(Environment.NewLine);
            }
        }

        private void WriteCopyright()
        {
            WriteLine("Copyright (C) 2006-2013 Richard Hubers.");
            WriteLine("Mix is open source software, see http://rh.github.com/mix/");
        }
    }
}
