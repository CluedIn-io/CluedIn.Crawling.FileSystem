using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CluedIn.Core;
using CluedIn.Core.Crawling;
using CluedIn.Core.Data.Relational;
using CluedIn.Core.Providers;
using CluedIn.Core.Webhooks;
using System.Configuration;
using CluedIn.Core.Configuration;
using CluedIn.Crawling.FileSystem;
using CluedIn.Crawling.FileSystem.Core;
using CluedIn.Crawling.FileSystem.Infrastructure.Factories;
using CluedIn.Providers.Models;

namespace CluedIn.Provider.FileSystem
{
  public class FileSystemProvider : ProviderBase
  {
    private readonly IFileSystemClientFactory _filesystemClientFactory;

    /**********************************************************************************************************
     * CONSTRUCTORS
     **********************************************************************************************************/

    public FileSystemProvider([NotNull] ApplicationContext appContext, IFileSystemClientFactory filesystemClientFactory)
        : base(appContext, FileSystemConstants.CreateProviderMetadata())
    {
      _filesystemClientFactory = filesystemClientFactory;
    }

    /**********************************************************************************************************
     * METHODS
     **********************************************************************************************************/

    public override async Task<CrawlJobData> GetCrawlJobData(
        ProviderUpdateContext context,
        IDictionary<string, object> configuration,
        Guid organizationId,
        Guid userId,
        Guid providerDefinitionId)
    {
        var jobData = new FileSystemCrawlJobData(configuration);
        if (jobData.StartingPoints == null)
            return null;

        //long fileCount = 0;
        //long folderCount = 0;

        //foreach (var folder in jobData.StartingPoints)
        //{
        //    fileCount += System.IO.Directory.GetFiles(folder.EntryPoint, "*.*", SearchOption.AllDirectories).Count();
        //    folderCount += System.IO.Directory.GetDirectories(folder.EntryPoint, "*.*", SearchOption.AllDirectories).Count();
        //}

        ////Remove the current folder
        //if (fileCount > 0)
        //    fileCount = fileCount - 1;

        //jobData.ExpectedTaskCount = fileCount + folderCount;

        var entityTypeStatistics = new ExpectedStatistics();
        //entityTypeStatistics.EntityTypeStatistics.Add(new EntityTypeStatistics(EntityType.Files.Directory, folderCount, 0));
        //entityTypeStatistics.EntityTypeStatistics.Add(new EntityTypeStatistics(EntityType.Files.File, fileCount, 0));
        jobData.ExpectedData = 0;
        jobData.ExpectedTime = new TimeSpan(0, 0, 0);
        //jobData.ExpectedTaskCount = folderCount + fileCount;
        jobData.ExpectedStatistics = entityTypeStatistics;

        return await Task.FromResult(jobData);
        }

    public override Task<bool> TestAuthentication(
        ProviderUpdateContext context,
        IDictionary<string, object> configuration,
        Guid organizationId,
        Guid userId,
        Guid providerDefinitionId)
    {
      throw new NotImplementedException();
    }

    public override Task<ExpectedStatistics> FetchUnSyncedEntityStatistics(ExecutionContext context, IDictionary<string, object> configuration, Guid organizationId, Guid userId, Guid providerDefinitionId)
    {
      throw new NotImplementedException();
    }

    public override async Task<IDictionary<string, object>> GetHelperConfiguration(
        ProviderUpdateContext context,
        [NotNull] CrawlJobData jobData,
        Guid organizationId,
        Guid userId,
        Guid providerDefinitionId)
    {
      if (jobData == null) throw new ArgumentNullException(nameof(jobData));

      var dictionary = new Dictionary<string, object>();

      if (jobData is FileSystemCrawlJobData filesystemCrawlJobData)
      {
        //TODO add the transformations from specific CrawlJobData object to dictionary
        // add tests to GetHelperConfigurationBehaviour.cs
//        dictionary.Add(FileSystemConstants.KeyName.ApiKey, filesystemCrawlJobData.ApiKey);
      }

      return await Task.FromResult(dictionary);
    }

    public override Task<IDictionary<string, object>> GetHelperConfiguration(
        ProviderUpdateContext context,
        CrawlJobData jobData,
        Guid organizationId,
        Guid userId,
        Guid providerDefinitionId,
        string folderId)
    {
      throw new NotImplementedException();  // TODO should this method be async ?
    }

    public override async Task<AccountInformation> GetAccountInformation(ExecutionContext context, [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
    {
      if (jobData == null) throw new ArgumentNullException(nameof(jobData));

      var filesystemCrawlJobData = jobData as FileSystemCrawlJobData;

      if (filesystemCrawlJobData == null)
      {
        throw new Exception("Wrong CrawlJobData type");
      }

      var client = _filesystemClientFactory.CreateNew(filesystemCrawlJobData);
      return await Task.FromResult(client.GetAccountInformation());
    }

    public override string Schedule(DateTimeOffset relativeDateTime, bool webHooksEnabled)
    {
      //TODO is this common for all providers?
      return webHooksEnabled && ConfigurationManager.AppSettings.GetFlag("Feature.Webhooks.Enabled", false) ? $"{relativeDateTime.Minute} 0/23 * * *"
          : $"{relativeDateTime.Minute} 0/4 * * *";
    }

    public override Task<IEnumerable<WebHookSignature>> CreateWebHook(ExecutionContext context, [NotNull] CrawlJobData jobData, [NotNull] IWebhookDefinition webhookDefinition, [NotNull] IDictionary<string, object> config)
    {
      if (jobData == null) throw new ArgumentNullException(nameof(jobData));
      if (webhookDefinition == null) throw new ArgumentNullException(nameof(webhookDefinition));
      if (config == null) throw new ArgumentNullException(nameof(config));

      throw new NotImplementedException();
    }

    public override Task<IEnumerable<WebhookDefinition>> GetWebHooks(ExecutionContext context)
    {
      throw new NotImplementedException();
    }

    public override Task DeleteWebHook(ExecutionContext context, [NotNull] CrawlJobData jobData, [NotNull] IWebhookDefinition webhookDefinition)
    {
      if (jobData == null) throw new ArgumentNullException(nameof(jobData));
      if (webhookDefinition == null) throw new ArgumentNullException(nameof(webhookDefinition));

      throw new NotImplementedException();
    }

    public override IEnumerable<string> WebhookManagementEndpoints([NotNull] IEnumerable<string> ids)
    {
      if (ids == null) throw new ArgumentNullException(nameof(ids));

      // TODO should ids also be checked for being empty ?

      throw new NotImplementedException();
    }

    public override async Task<CrawlLimit> GetRemainingApiAllowance(ExecutionContext context, [NotNull] CrawlJobData jobData, Guid organizationId, Guid userId, Guid providerDefinitionId)
    {
      if (jobData == null) throw new ArgumentNullException(nameof(jobData));

      //TODO what the hell is this?
      //There is no limit set, so you can pull as often and as much as you want.
      return await Task.FromResult(new CrawlLimit(-1, TimeSpan.Zero));
    }
  }
}
