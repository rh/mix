using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using Mix.Core.Attributes;

namespace Mix.Console.Commands
{
    internal sealed class HelpCommand : Command
    {
        private string name = String.Empty;

        public string Name
        {
            get { return name; }
            set { name = (value != null ? value.ToLower() : String.Empty); }
        }

        public override int Execute()
        {
            if (String.IsNullOrEmpty(name))
            {
                WriteLine("Mix, The XML Refactoring Tool, version {0}", Application.ProductVersion);
                WriteLine("See http://mix.sourceforge.net/");
                WriteLine("Usage: mix <action> [arguments]");
                WriteLine("Type 'mix version' to see the program version.");
                WriteLine("Type 'mix list' to see a list of all available actions.");
                WriteLine("Type 'mix help <action>' for help on a specific action.");
            }
            else
            {
                if (CommandFactory.Commands.ContainsKey(name))
                {
                    object obj = CommandFactory.Commands[name];

                    if (obj is ActionCommand)
                    {
                        obj = (obj as ActionCommand).Action;
                    }

                    WriteDescription(obj);
                    Writeproperties(obj);
                }
                else
                {
                    WriteError("Unknown action:");
                    WriteLine(" '{0}'", name);
                    WriteLine("Type 'mix list' to see a list of all available actions.");
                    return 1;
                }
            }
            return 0;
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

        private void Writeproperties(object obj)
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
                WriteLine("Arguments:");
                foreach (PropertyInfo property in properties)
                {
                    string required = RequiredAttribute.IsDefinedOn(property) ? "[required] " : "";
                    string description = DescriptionAttribute.GetDescriptionFrom(property);
                    WriteLine("  {0}: {2}{1}", property.Name.ToLower(), description, required);
                }
            }
        }
    }
}
