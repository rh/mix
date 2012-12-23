using System;
using System.Collections.Generic;
using System.IO;
using Mix.Exceptions;

namespace Mix
{
	public class PathExpander
	{
		private readonly IList<string> files = new List<string>();
		private const SearchOption TopDirectoryOnly = SearchOption.TopDirectoryOnly;

		public IList<string> Expand(string workingDirectory, string patterns, bool recursively)
		{
			if (patterns == null)
			{
				return files;
			}
			return Expand(workingDirectory, patterns.Split(new[] {Path.PathSeparator}, StringSplitOptions.RemoveEmptyEntries), recursively);
		}

		/// <summary>
		/// This methods resolves the full paths from all files represented by <paramref name="patterns"/>.
		/// Relative paths are resolved relative to <paramref name="workingDirectory"/>.
		/// </summary>
		/// <param name="workingDirectory">The directory which relative paths are relative to.</param>
		/// <param name="patterns">A list of absolute or relative directories and/or files and/or patterns.</param>
		/// <param name="recursively">If <c>true</c>, paths are resolved recursively.</param>
		/// <returns></returns>
		public IList<string> Expand(string workingDirectory, IEnumerable<string> patterns, bool recursively)
		{
			foreach (var pattern in patterns)
			{
				try
				{
					if (Directory.Exists(pattern))
					{
						// Examples of 'pattern' that may lead here:
						//   C:\Projects\temp
						//   temp
						//   ..\temp
						//   .
						AddFromDirectory(pattern, "*.xml", recursively);
						continue;
					}
				}
				catch (IOException)
				{
					// Mono might throw an IOException when using a pattern:
					// Win32 IO returned ERROR_INVALID_NAME. Path: *.xml
				}

				if (File.Exists(pattern))
				{
					// Examples of 'pattern' that may lead here:
					//   C:\Projects\Mix\test.xml
					//   test.xml
					//   ..\temp\test.xml
					AddFromFile(pattern);
				}
				else
				{
					string path = null;
					try
					{
						path = Path.GetDirectoryName(pattern);
					}
					catch (ArgumentException e)
					{
						if (pattern.Trim().Length == 0)
						{
							continue;
						}

						ThrowInvalidPathException(pattern, e);
					}
					catch (PathTooLongException e)
					{
						ThrowInvalidPathException("'{0}' is too long.", pattern, e);
					}

					try
					{
						if (Directory.Exists(path))
						{
							// Examples of 'pattern' that may lead here:
							//   C:\Projects\Mix\*.xml
							AddFromDirectory(path, Path.GetFileName(pattern), recursively);
						}
					}
					catch (IOException)
					{
						// Mono might throw an IOException when using an invalid character:
						// Win32 IO returned ERROR_INVALID_NAME. Path: :
					}

					// Examples of 'pattern' that may lead here:
					//   *.xml
					try
					{
						// Since 'path = Path.GetDirectoryName(pattern)' above results in path being null,
						// only a pattern is specified, e.g. '*.xml'
						AddFromDirectory(workingDirectory, pattern.Trim(), recursively);
					}
					catch (IOException e)
					{
						ThrowInvalidPathException(pattern, e);
					}
					catch (ArgumentException e)
					{
						ThrowInvalidPathException(pattern, e);
					}
				}
			}

			return Uniquefy(files);
		}

		private void AddFromDirectory(string path, string searchPattern, bool recursively)
		{
			foreach (var file in new DirectoryInfo(path).GetFiles(searchPattern, TopDirectoryOnly))
			{
				files.Add(file.FullName);
			}

			if (recursively)
			{
				foreach (var directory in new DirectoryInfo(path).GetDirectories())
				{
					AddFromDirectory(directory.FullName, searchPattern, true);
				}
			}
		}

		private void AddFromFile(string fileName)
		{
			files.Add(new FileInfo(fileName).FullName);
		}

		private static IList<string> Uniquefy(IEnumerable<string> list)
		{
			var uniques = new List<string>();
			foreach (var item in list)
			{
				if (!uniques.Contains(item))
				{
					uniques.Add(item);
				}
			}
			return uniques;
		}

		private static void ThrowInvalidPathException(string pattern, Exception exception)
		{
			ThrowInvalidPathException("'{0}' is not a valid filename or pattern.", pattern, exception);
		}

		private static void ThrowInvalidPathException(string format, string pattern, Exception exception)
		{
			var message = String.Format(format, pattern);
			throw new InvalidPathException(message, exception);
		}
	}
}
