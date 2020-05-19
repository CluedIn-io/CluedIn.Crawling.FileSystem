using Castle.Windsor;
using CluedIn.Core;
using CluedIn.Core.Providers;
using CluedIn.Crawling.FileSystem.Infrastructure.Factories;
using Moq;

namespace Provider.FileSystem.Test.FileSystemProvider
{
  public abstract class FileSystemProviderTest
  {
    protected readonly ProviderBase Sut;

    protected Mock<IFileSystemClientFactory> NameClientFactory;
    protected Mock<IWindsorContainer> Container;

    protected FileSystemProviderTest()
    {
      Container = new Mock<IWindsorContainer>();
      NameClientFactory = new Mock<IFileSystemClientFactory>();
      var applicationContext = new ApplicationContext(Container.Object);
      Sut = new CluedIn.Provider.FileSystem.FileSystemProvider(applicationContext, NameClientFactory.Object);
    }
  }
}
