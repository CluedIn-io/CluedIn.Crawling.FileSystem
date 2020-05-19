// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileInfoBlobIndexer.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FileInfoBlobIndexer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;

using CluedIn.Core;
using CluedIn.Core.Agent.Jobs;
using CluedIn.Core.Configuration;
using CluedIn.Core.Data;

using System.Configuration;

namespace CluedIn.Crawling.FileSystem.Indexing
{
    /// <summary>The file info blob indexer.</summary>
    public class FileInfoBlobIndexer : IFileInfoBlobIndexer
    {
        private readonly IAgentJobProcessorArguments args;
        private readonly ApplicationContext context;

        public FileInfoBlobIndexer([NotNull] IAgentJobProcessorArguments args, [NotNull] ApplicationContext context)
        {
            this.args = args ?? throw new ArgumentNullException(nameof(args));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Index(FileInfo item, Clue clue, AgentJobProcessorState<FileSystemCrawlJobData> state)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (clue == null)
                throw new ArgumentNullException(nameof(clue));

            if (item.Exists == false)
                return;

            if (item.Length == 0)
                return;

            if (!ConfigurationManager.AppSettings.GetFlag("Crawl.InitialCrawl.FileIndexing", true))
                return;

            FileCrawlingUtility.IndexFile(item, clue.Data, clue, args, context);
        }
    }
}
