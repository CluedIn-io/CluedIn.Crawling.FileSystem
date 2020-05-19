using CluedIn.Crawling.FileSystem.Core;

namespace CluedIn.Crawling.FileSystem
{
    public class FileSystemCrawlerJobProcessor : GenericCrawlerTemplateJobProcessor<FileSystemCrawlJobData>
    {
        public FileSystemCrawlerJobProcessor(FileSystemCrawlerComponent component) : base(component)
        {
        }
    }
}
