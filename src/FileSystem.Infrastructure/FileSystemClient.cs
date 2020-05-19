using System;
using CluedIn.Core.Logging;
using CluedIn.Core.Providers;
using CluedIn.Crawling.FileSystem.Core;
using CluedIn.Crawling.FileSystem.Models;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.Devices;
using RestSharp;

namespace CluedIn.Crawling.FileSystem.Infrastructure
{
  public class FileSystemClient
  {
    private const string s_baseUri = "http://sample.com";

    // ReSharper disable once NotAccessedField.Local
    private readonly ILogger _log;

    private FileSystemCrawlJobData _filesystemCrawlJobData;

    public FileSystemClient(ILogger log, FileSystemCrawlJobData filesystemCrawlJobData, IRestClient client) // TODO: pass on any extra dependencies
    {
        if (filesystemCrawlJobData == null) throw new ArgumentNullException(nameof(filesystemCrawlJobData));
       if (client == null) throw new ArgumentNullException(nameof(client));

       _log = log ?? throw new ArgumentNullException(nameof(log));

       _filesystemCrawlJobData = filesystemCrawlJobData;
      // TODO use info from filesystemCrawlJobData to instantiate the connection
      client.BaseUrl = new Uri(s_baseUri);
//      client.AddDefaultParameter("api_key", filesystemCrawlJobData.ApiKey, ParameterType.QueryString);
    }
    public IEnumerable<ComputerInfo> GetComputerInfo()
    {
      return new[] { new ComputerInfo() };
    }

    public AccountInformation GetAccountInformation()
    {
      return new AccountInformation("", ""); //TODO
    }
  }
}
