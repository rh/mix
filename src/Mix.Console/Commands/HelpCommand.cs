using System;
using System.Collections.Generic;
using System.Reflection;
using Mix.Core.Attributes;

namespace Mix.Console.Commands
{
    public class HelpCommand : Command
    {
        private string name = String.Empty;

        #region Constructors

        public HelpCommand()
        {
        }

        public HelpCommand(string name)
        {
            this.name = name;
        }

        #endregion

        public override int Execute()
        {
            if (ActionIsNotSet)
            {
                WriteUsage();
            }
            else
            {
                if (ActionIsKnown)
                {
                    WriteActionUsage();
                }
                else
                {
                    WriteUnknownActionUsage();
                    return 1;
                }
            }
            return 0;
        }

        private bool ActionIsNotSet
        {
            get { return String.IsNullOrEmpty(name); }
        }

        private bool ActionIsKnown
        {
            get { return CommandFactory.Commands.ContainsKey(name); }
        }

        private void WriteUsage()
        {
            WriteLine("Usage: mix <action> [arguments]");
            Write(Environment.NewLine);
            WriteLine("Mix is a tool for XML refactoring.");
            WriteLine("For additional information, see http://mix.sourceforge.net/");
            Write(Environment.NewLine);
            WriteLine("Type 'mix version' to see the program version.");
            WriteLine("Type 'mix list' to see a list of all available actions.");
            WriteLine("Type 'mix help <action>' for help on a specific action.");
        }

        private void WriteActionUsage()
        {
            object obj = CommandFactory.Commands[name];

            if (obj is ActionCommand)
            {
                obj = (obj as ActionCommand).Action;
            }

            WriteDescription(obj);
            WriteProperties(obj);
        }

        private void WriteUnknownActionUsage()
        {
            WriteLine("Unknown action: '{0}'", name);
            WriteLine("Type 'mix list' to see a list of all available actions.");
        }

        private void WriteDescription(object obj)
        {
            if (DescriptionAttribute.IsDefinedOn(obj))
            {
                string description = DescriptionAttribute.GetDescriptionFrom(obj);
                WriteLine("{0}: {1}", name, description);
            }
            else
            {
                WriteLine("{0}: [no description]", name);
            }
        }

        private void WriteProperties(object obj)
        {
            List<PropertyInfo> properties = new List<PropertyInfo>();

            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                if (ArgumentAttribute.IsDefinedOn(property))
                {
                    properties.Add(property);
                }
            }

            if (properties.Count > 0)
            {
                Write(Environment.NewLine);
                WriteLine("Arguments:");
                foreach (PropertyInfo property in properties)
                {
                    string required = RequiredAttribute.IsDefinedOn(property) ?
                                      String.Format("{0}{1,-17}{2}", Environment.NewLine, "", "[required]") : "";
                    string description = DescriptionAttribute.GetDescriptionFrom(property);
                    WriteLine("  {0,-15}{1}{2}", property.Name.ToLower(), description, required);
                }
            }
        }
    }
}