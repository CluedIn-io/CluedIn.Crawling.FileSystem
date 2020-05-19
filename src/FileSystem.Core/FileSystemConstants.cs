using System;
using System.Collections.Generic;
using CluedIn.Core.Net.Mail;
using CluedIn.Core.Providers;

namespace CluedIn.Crawling.FileSystem.Core
{
  public class FileSystemConstants
  {
    public struct KeyName
    {
      public static readonly string ApiKey = nameof(ApiKey);
    }

    public const string CodeOrigin = "FileSystem";
    public const string ProviderRootCodeValue = "FileSystem";
    public const string CrawlerName = "FileSystemCrawler";
    public const string CrawlerComponentName = "FileSystemCrawler";
    public const string CrawlerDescription = "FileSystem will allow you to pull in data to CluedIn from any Operating System FileSystem";
    public const string CrawlerDisplayName = "FileSystem";  // TODO RJ - this field is never used can it be removed ?
    public const string Uri = "http://www.sampleurl.com";

     

    public static readonly Guid ProviderId = Guid.Parse("09b98efd-d926-452f-a967-18595c2201d2");
    public const string ProviderName = "FileSystem";
    public const bool SupportsConfiguration = true;
    public const bool SupportsWebHooks = false;
    public const bool SupportsAutomaticWebhookCreation = true;
    public const bool RequiresAppInstall = false;
    public const string AppInstallUrl = null;
    public const string ReAuthEndpoint = null;
    public const string IconUri = "https://s3-eu-west-1.amazonaws.com/staticcluedin/bitbucket.png";

    public static readonly ComponentEmailDetails ComponentEmailDetails = new ComponentEmailDetails
    {
      Features = new Dictionary<string, string>
            {
                                       { "Tracking",        "Expenses and Invoices against customers" },
                                       { "Intelligence",    "Aggregate types of invoices and expenses against customers and companies." }
                                   },
      Icon = new Uri(IconUri),
      ProviderName = ProviderName,
      ProviderId = ProviderId,
      Webhooks = SupportsWebHooks
    };

    public static IProviderMetadata CreateProviderMetadata()
    {
      return new ProviderMetadata
      {
        Id = ProviderId,
        ComponentName = CrawlerName,
        Name = ProviderName,
        Type = "Cloud",
        SupportsConfiguration = SupportsConfiguration,
        SupportsWebHooks = SupportsWebHooks,
        SupportsAutomaticWebhookCreation = SupportsAutomaticWebhookCreation,
        RequiresAppInstall = RequiresAppInstall,
        AppInstallUrl = AppInstallUrl,
        ReAuthEndpoint = ReAuthEndpoint,
        ComponentEmailDetails = ComponentEmailDetails
      };
    }
  }
}
