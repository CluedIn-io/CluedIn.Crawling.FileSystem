using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CluedIn.Core.Crawling;
using CluedIn.Crawling.FileSystem.Core;
using CluedIn.Crawling.FileSystem.Core.Models;
using CluedIn.Crawling.FileSystem.Infrastructure.Factories;

namespace CluedIn.Crawling.FileSystem
{
    public class FileSystemCrawler : ICrawlerDataGenerator
    {
        private readonly IFileSystemClientFactory _clientFactory;
        public FileSystemCrawler(IFileSystemClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IEnumerable<object> GetData(CrawlJobData jobData)
        {
            var filesystemcrawlJobData = jobData as FileSystemCrawlJobData;
            if(filesystemcrawlJobData == null)
            {
                yield break;
            }

            var client = _clientFactory.CreateNew(filesystemcrawlJobData);

            //crawl data from provider and yield objects

            foreach( var computerInfo in client.GetComputerInfo())
            {
                yield return computerInfo;
            }

            foreach (var item in filesystemcrawlJobData.StartingPoints)
            {
                var info = new DirectoryInfo(item.EntryPoint);

                CrawlDirectory(info, item.CrawlOptions, filesystemcrawlJobData);
            }
        }

        protected IEnumerable<object> CrawlDirectory(DirectoryInfo info, CrawlOptions options, FileSystemCrawlJobData filesystemcrawlJobData)
        {
            var directory = new FileSystemItem<DirectoryInfo>(info, filesystemcrawlJobData);

            yield return directory.Owner;

            yield return directory;

            foreach (var fileInfo in info.GetFiles())
            {

                var file = new FileSystemItem<FileInfo>(fileInfo, filesystemcrawlJobData);

                yield return file.Owner;

                yield return file;
            }

            if (options == CrawlOptions.Recursive)
            {
                foreach (var subDirectory in FilterFileSystemInfos(info.GetDirectories().OrderBy(d => Guid.NewGuid()), filesystemcrawlJobData))
                {
                    CrawlDirectory(subDirectory, options, filesystemcrawlJobData);
                }
            }
        }

        protected IEnumerable<T> FilterFileSystemInfos<T>(IEnumerable<T> items, FileSystemCrawlJobData filesystemCrawlJobData)
            where T : FileSystemInfo
        {
            return items.Where(
                c =>
                    FileSystemInfoFilter.ShouldIndex(c)
                    && (
                        filesystemCrawlJobData.LastCrawlFinishTime == DateTimeOffset.MinValue
                        || c.LastWriteTimeUtc > filesystemCrawlJobData.LastCrawlFinishTime.UtcDateTime
                        || c.CreationTimeUtc > filesystemCrawlJobData.LastCrawlFinishTime.UtcDateTime
                    )
            );
        }
    }
}
