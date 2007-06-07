using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Mix.Core.Attributes;

namespace Mix.Console.Commands
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
            WriteLine("Mix, version: {0}", Application.ProductVersion);
        }

        private void WriteCompilationDate()
        {
            FileInfo fileInfo;

            try
            {
                fileInfo = new FileInfo(Assembly.GetEntryAssembly().Location);
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

                CultureInfo ci = new CultureInfo("en-US");
                WriteLine("  compiled {0}", date.ToString("MMMM %d yyyy, HH:mm:ss", ci));
                Write(Environment.NewLine);
            }
        }

        private void WriteCopyright()
        {
            WriteLine("Copyright (C) 2006-2007 Richard Hubers.");
            WriteLine("Mix is open source software, see http://mix.sourceforge.net/");
        }
    }
}