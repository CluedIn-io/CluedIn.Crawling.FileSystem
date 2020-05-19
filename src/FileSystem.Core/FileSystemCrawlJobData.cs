// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileSystemCrawlJobData.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FileSystemCrawlJobData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using CluedIn.Core;
using CluedIn.Core.Crawling;
using CluedIn.Crawling.FileSystem.Helpers;

using Newtonsoft.Json.Linq;

namespace CluedIn.Crawling.FileSystem
{
    /// <summary>The file system crawl job data.</summary>
    public class FileSystemCrawlJobData : CrawlJobData
    {
        public FileSystemCrawlJobData()
        {
            StartingPoints = new List<CrawlEntry>();
            IgnoredFileTypes = new List<string>();
            FileSizeLimit = Constants.MaxFileIndexingFileSize;
        }

        public IList<CrawlEntry> StartingPoints { get; set; }

        public IList<string> IgnoredFileTypes { get; set; }

        public long? FileSizeLimit { get; set; }

        public bool FetchOwners { get; set; } = true;

        // TODO: Remove this when proper restrictions have been implemented
        public bool TempRunOnSharedProcessors { get; set; } = false;

        public LookupCache LookupCache { get; set; }

        public FileSystemCrawlJobData(IDictionary<string, object> configuration)
        {
            if (configuration == null)
            {
                StartingPoints = null;
                return;
            }

            if (!configuration.ContainsKey("startingPoints"))
            {
                StartingPoints = null;
                return;
            }

            try
            {
                var start = (JObject)configuration["startingPoints"];

                JToken token;
                if (start.TryGetValue("name", out token))
                {
                    this.StartingPoints = new List<CrawlEntry>()
                                              {
                                                  new CrawlEntry()
                                                      {
                                                          CrawlOptions = configuration.ContainsKey("crawlOptions")
                                                                             ? ((CrawlOptions) configuration["crawlOptions"])
                                                                             : CrawlOptions.Recursive,
                                                          EntryPoint = token.Value<string>(),
                                                          IndexingOptions = IndexingOptions.Index,
                                                          CrawlPriority = configuration.ContainsKey("crawlPriority")
                                                                              ? ((CrawlPriority) configuration["crawlPriority"])
                                                                              : CrawlPriority.Normal
                                                      }
                                              };
                }

                // If there is no value then resort to ignoring no file formats.
                IgnoredFileTypes = configuration.ContainsKey("ignoredFileTypes")
                                            ? ((JArray)configuration["ignoredFileTypes"]).ToList().Select(
                                                startPoint => startPoint.ToString()).ToList()
                                            : new List<string>();

                // If there is no value then default to 5 MB files.
                FileSizeLimit = configuration.ContainsKey("fileSizeLimit")
                                         ? long.Parse(configuration["fileSizeLimit"].ToString()) as long?
                                         : Constants.MaxFileIndexingFileSize;

                LastCrawlFinishTime = ReadLastCrawlFinishTime(configuration);

                FetchOwners = GetValue(configuration, "fetchOwners", this.FetchOwners);
                TempRunOnSharedProcessors = GetValue(configuration, "runOnSharedProcessors", false);
            }
            catch (Exception)
            {
                StartingPoints = null;
            }
        }
    }
}
