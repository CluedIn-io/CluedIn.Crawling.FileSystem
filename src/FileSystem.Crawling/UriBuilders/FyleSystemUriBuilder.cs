// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileSystemInfoUriBuilder.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FileSystemInfoUriBuilder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Text.RegularExpressions;

using CluedIn.Core;

namespace CluedIn.Crawling.FileSystem.UriBuilders
{
    /// <summary>The file system info uri builder.</summary>
    public class FileSystemInfoUriBuilder : IFileSystemInfoUriBuilder
    {
        // TODO: Perform better local filepath translation.
        // TODO: Cater for which os the crawling is running on
        private readonly Regex pattern = new Regex(@"file:///(?<drive>[a-zA-Z]):/(?<path>.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);

        public Uri GetUri([NotNull] FileSystemInfo item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var result = new Uri(item.FullName, UriKind.Absolute);

            if (result.IsLoopback && string.IsNullOrEmpty(result.Host))
            {
                var match = this.pattern.Match(result.AbsoluteUri);

                if (match.Success)
                {
                    var drive = match.Groups["drive"].Value;
                    var path = match.Groups["path"].Value;

                    result = new Uri(string.Format("file://{0}/{1}$/{2}", Environment.MachineName, drive, path));
                }
            }

            return result;
        }
    }
}
