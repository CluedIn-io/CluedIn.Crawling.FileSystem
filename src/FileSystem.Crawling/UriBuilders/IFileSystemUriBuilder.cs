using System;
using System.IO;

namespace CluedIn.Crawling.FileSystem.UriBuilders
{
    public interface IFileSystemInfoUriBuilder
    {
        Uri GetUri(FileSystemInfo item);
    }
}
