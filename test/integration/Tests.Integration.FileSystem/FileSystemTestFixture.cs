using CluedIn.Crawling;
using CluedIn.Crawling.FileSystem.Core;
using System.IO;
using System.Reflection;
using CrawlerIntegrationTesting.Clues;

namespace Tests.Integration.FileSystem
{
    public class FileSystemTestFixture
    {
        public FileSystemTestFixture()
        {
            var executingFolder = new FileInfo(Assembly.GetExecutingAssembly().CodeBase.Substring(8)).DirectoryName;
            var p = new DebugCrawlerHost<FileSystemCrawlJobData>(executingFolder, FileSystemConstants.ProviderName);

            ClueStorage = new ClueStorage();

            p.ProcessClue += ClueStorage.AddClue;            

            p.Execute(FileSystemConfiguration.Create(), FileSystemConstants.ProviderId);
        }

        public ClueStorage ClueStorage { get; }

        public void Dispose()
        {
        }

    }
}


