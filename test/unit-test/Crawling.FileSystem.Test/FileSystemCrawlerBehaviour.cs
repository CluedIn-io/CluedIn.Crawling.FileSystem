using CluedIn.Core.Crawling;
using CluedIn.Crawling;
using CluedIn.Crawling.FileSystem;
using CluedIn.Crawling.FileSystem.Infrastructure.Factories;
using Moq;
using Should;
using Xunit;

namespace Crawling.FileSystem.Test
{
  public class FileSystemCrawlerBehaviour
  {
    private readonly ICrawlerDataGenerator _sut;

    public FileSystemCrawlerBehaviour()
    {
        var nameClientFactory = new Mock<IFileSystemClientFactory>();

        _sut = new FileSystemCrawler(nameClientFactory.Object);
    }

    [Fact]
    public void GetDataReturnsData()
    {
      var jobData = new CrawlJobData();

      _sut.GetData(jobData)
          .ShouldNotBeNull();
    }
  }
}
