// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileSystemInfoFilter.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FileSystemInfoFilter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;

namespace CluedIn.Crawling.FileSystem
{
    /// <summary>
    /// The file system info filter.
    /// </summary>
    public static class FileSystemInfoFilter
    {
        public static bool ShouldIndex(FileSystemInfo fileSystemInfo)
        {
            return fileSystemInfo.Attributes.HasFlag(FileAttributes.System) == false
                   && fileSystemInfo.Attributes.HasFlag(FileAttributes.Hidden) == false
                   && fileSystemInfo.Exists
                   && fileSystemInfo.Name?.StartsWith("~") != true
                   && fileSystemInfo.Name?.StartsWith(".dat.nosync") != true
                   && fileSystemInfo.Name?.StartsWith(".afpDeleted") != true
                   && fileSystemInfo.Name?.Equals(".DS_Store", StringComparison.InvariantCultureIgnoreCase) != true
                   && fileSystemInfo.Name?.Equals(".git", StringComparison.InvariantCultureIgnoreCase) != true
                   && fileSystemInfo.Name?.Equals(".vs", StringComparison.InvariantCultureIgnoreCase) != true
                   && fileSystemInfo.Name?.Equals("Thumbs.db", StringComparison.InvariantCultureIgnoreCase) != true
                   && fileSystemInfo?.Extension.Equals("tmp", StringComparison.InvariantCultureIgnoreCase) != true
                   && fileSystemInfo?.Extension.Equals("idlk", StringComparison.InvariantCultureIgnoreCase) != true
                ;
        }
    }
}
