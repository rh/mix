using System;
using System.Collections.Generic;
using System.IO;
using log4net;
using Mix.Console.Exceptions;

namespace Mix.Console
{
    public class PathExpander
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PathExpander));
        readonly IList<string> files = new List<string>();
        private const SearchOption searchOption = SearchOption.TopDirectoryOnly;

        public IList<string> Expand(string workingDirectory, string patterns)
        {
            if (patterns == null)
            {
                return files;
            }
            return Expand(workingDirectory, patterns.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries));
        }

        /// <summary>
        /// This methods resolves the full paths from all files represented by <paramref name="patterns"/>.
        /// Relative paths are resolved relative to <paramref name="workingDirectory"/>.
        /// </summary>
        /// <param name="workingDirectory">The directory which relative paths are relative to.</param>
        /// <param name="patterns">A list of absolute or relative directories and/or files and/or patterns.</param>
        /// <returns></returns>
        public IList<string> Expand(string workingDirectory, IEnumerable<string> patterns)
        {
            foreach (var pattern in patterns)
            {
                if (Directory.Exists(pattern))
                {
                    // Examples of 'pattern' that may lead here:
                    //   C:\Projects\temp
                    //   temp
                    //   ..\temp
                    //   .
                    AddFromDirectory(pattern, "*.xml");
                }
                else if (File.Exists(pattern))
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

                    if (path != null && Directory.Exists(path))
                    {
                        // Examples of 'pattern' that may lead here:
                        //   C:\Projects\Mix\*.xml
                        AddFromDirectory(path, Path.GetFileName(pattern));
                    }
                    else
                    {
                        // Examples of 'pattern' that may lead here:
                        //   *.xml
                        try
                        {
                            // Since 'path = Path.GetDirectoryName(pattern)' above results in path being null,
                            // only a pattern is specified, e.g. '*.xml'
                            AddFromDirectory(workingDirectory, pattern.Trim());
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
            }

            return Uniquefy(files);
        }

        private void AddFromDirectory(string path, string searchPattern)
        {
            var directory = new DirectoryInfo(path);
            foreach (var file in directory.GetFiles(searchPattern, searchOption))
            {
                files.Add(file.FullName);
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
            log.Error(message, exception);
            throw new InvalidPathException(message, exception);
        }
    }
}