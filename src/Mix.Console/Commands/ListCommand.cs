using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Mix.Core;
using Mix.Core.Attributes;

namespace Mix.Console.Commands
{
	[Description("Shows a list of all available actions.")]
	internal sealed class ListCommand : Command
	{
		public override int Execute()
		{
			WriteLine("Available actions:");
			foreach (Type type in Actions())
			{
				WriteLine("  {0}", type.Name.Replace("Action", "").ToLower());
			}
			WriteLine("\nType 'mix help <action>' for help on a specific action.");
			return 0;
		}

		private static IList<Type> types;

		public static IList<Type> Actions()
		{
			if (types == null)
			{
				types = new List<Type>();
				// Explicitly load assembly Mix.Actions
				Assembly.Load("Mix.Actions");
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				AddActions(assemblies);
			}
			return types;
		}

		private static void AddActions(Assembly[] assemblies)
		{
			Check.ArgumentIsNotNull(assemblies, "assemblies");

			foreach (Assembly assembly in assemblies)
			{
				if (IsSystemAssembly(assembly))
				{
					continue;
				}

				foreach (Type type in assembly.GetTypes())
				{
					if (IsAction(type))
					{
						if (!types.Contains(type))
						{
							types.Add(type);
						}
					}
				}
			}
		}

		[DebuggerStepThrough]
		private static bool IsSystemAssembly(Assembly assembly)
		{
			string name = assembly.GetName().ToString().ToLower();
			return name.StartsWith("system") ||
			       name.StartsWith("microsoft") ||
			       name.StartsWith("vshost") ||
			       name == "mscorlib";
		}

		[DebuggerStepThrough]
		private static bool IsAction(Type type)
		{
			return typeof(Action).IsAssignableFrom(type) &&
			       !type.IsInterface &&
			       !type.IsAbstract;
		}
	}
}
