using CluedIn.Crawling.FileSystem.Core;

namespace CluedIn.Crawling.FileSystem.Infrastructure.Factories
{
    public interface IFileSystemClientFactory
    {
        FileSystemClient CreateNew(FileSystemCrawlJobData filesystemCrawlJobData);
    }
}
