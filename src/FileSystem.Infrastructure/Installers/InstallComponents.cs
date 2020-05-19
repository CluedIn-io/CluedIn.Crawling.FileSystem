using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Facilities.TypedFactory;

using CluedIn.Core;
using CluedIn.Crawling.FileSystem.Infrastructure.Factories;
using RestSharp;

namespace CluedIn.Crawling.FileSystem.Infrastructure.Installers
{
  public class InstallComponents : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container
          .AddFacilityIfNotExists<TypedFactoryFacility>()
          .Register(Component.For<IFileSystemClientFactory>().AsFactory())
          .Register(Component.For<FileSystemClient>().LifestyleTransient());

      if (!container.Kernel.HasComponent(typeof(IRestClient)) && !container.Kernel.HasComponent(typeof(RestClient)))
        container.Register(Component.For<IRestClient, RestClient>());
    }
  }
}
