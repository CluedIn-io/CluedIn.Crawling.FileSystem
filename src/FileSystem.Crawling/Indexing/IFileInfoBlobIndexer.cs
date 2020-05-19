// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileInfoBlobIndexer.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the IFileInfoBlobIndexer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;

using CluedIn.Core;
using CluedIn.Core.Agent.Jobs;
using CluedIn.Core.Data;

namespace CluedIn.Crawling.FileSystem.Indexing
{
    /// <summary>The FileInfoBlobIndexer interface.</summary>
    public interface IFileInfoBlobIndexer
    {
        void Index([NotNull] FileInfo item, [NotNull] Clue clue, AgentJobProcessorState<FileSystemCrawlJobData> state);
    }
}
