using System.Windows.Forms;
using Mix.Core.Attributes;

namespace Mix.Console.Commands
{
    [Description("Shows the version of this application.")]
    public class VersionCommand : Command
    {
        public override int Execute()
        {
            WriteLine("Version: {0}", Application.ProductVersion);
            return 0;
        }
    }
}