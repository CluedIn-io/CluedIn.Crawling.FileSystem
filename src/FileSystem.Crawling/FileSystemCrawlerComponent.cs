using CluedIn.Core;
using CluedIn.Crawling.FileSystem.Core;

using ComponentHost;

namespace CluedIn.Crawling.FileSystem
{
    [Component(FileSystemConstants.CrawlerComponentName, "Crawlers", ComponentType.Service, Components.Server, Components.ContentExtractors, Isolation = ComponentIsolation.NotIsolated)]
    public class FileSystemCrawlerComponent : CrawlerComponentBase
    {
        public FileSystemCrawlerComponent([NotNull] ComponentInfo componentInfo)
            : base(componentInfo)
        {
        }
    }
}

