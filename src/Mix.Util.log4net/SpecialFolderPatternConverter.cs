using System;
using System.IO;
using log4net.Util;

namespace Mix.Util.log4net
{
    public class SpecialFolderPatternConverter : PatternConverter
    {
        protected override void Convert(TextWriter writer, object state)
        {
            Environment.SpecialFolder specialFolder =
                (Environment.SpecialFolder) Enum.Parse(typeof(Environment.SpecialFolder), base.Option, true);
            writer.Write(Environment.GetFolderPath(specialFolder));
        }
    }
}
