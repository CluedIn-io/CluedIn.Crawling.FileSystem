using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CluedIn.Core;
using CluedIn.Core.Crawling;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.FileSystem.Core;
using CluedIn.Crawling.Helpers;

using CluedIn.Crawling.FileSystem.Vocabularies;
using CluedIn.Crawling.FileSystem.Core.Models;
using CluedIn.Crawling.FileSystem.Indexing;
using CluedIn.Crawling.FileSystem.UriBuilders;
using RuleConstants = CluedIn.Core.Constants.Validation.Rules;

namespace CluedIn.Crawling.FileSystem.ClueProducers
{
    public class FileInfoClueProducer : BaseClueProducer<FileSystemItem<FileInfo>>
    {
        private readonly IClueFactory _factory;
        private readonly FileSystemCrawlJobData _crawlJobData;

        public FileInfoClueProducer(IClueFactory factory, FileSystemCrawlJobData crawlJobData)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _crawlJobData = crawlJobData ?? throw new ArgumentNullException(nameof(crawlJobData));
        }

        protected override Clue MakeClueImpl(FileSystemItem<FileInfo> input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var value = input.Item;
            var uriBuilder = new FileSystemInfoUriBuilder();
//            var indexer = new FileInfoBlobIndexer();

            var clue = _factory.Create(EntityType.Files.File, uriBuilder.GetUri(value).AbsoluteUri, accountId);

            var uri = uriBuilder.GetUri(value);

            var data = clue.Data;

            data.EntityData.Name = value.Name; // + crawl.FileSystemInfo.Extension;
            data.EntityData.DisplayName = value.Name;
            data.EntityData.Uri = uri;
            data.EntityData.Culture = CultureInfo.InvariantCulture;

            var attributes = value.Attributes;

            var isReadOnly = (attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
            var isHidden = (attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
            var isArchive = (attributes & FileAttributes.Archive) == FileAttributes.Archive;
            var isSystem = (attributes & FileAttributes.System) == FileAttributes.System;

            var vocab = new FileInfoVocabulary();
            var securityVocab = new FileSecurityVocabulary();

            data.EntityData.Properties[vocab.IsReadOnly] = isReadOnly.ToString();
            data.EntityData.Properties[vocab.IsHidden] = isHidden.ToString();
            data.EntityData.Properties[vocab.IsArchive] = isArchive.ToString();
            data.EntityData.Properties[vocab.IsSystem] = isSystem.ToString();

            data.EntityData.Properties[securityVocab.OwnerSID] = input.Owner.PrintIfAvailable(v => v.Sid.ToString());
            data.EntityData.Properties[securityVocab.OwnerNTAccount] = input.Owner.PrintIfAvailable(v => v.Account?.ToString());
            data.EntityData.Properties[securityVocab.OwnerDisplayName] = input.Owner.PrintIfAvailable(v => v.Principal?.DisplayName);

            data.EntityData.Properties[vocab.CreationTimeUtc] = value.CreationTimeUtc.ToString("o");
            data.EntityData.Properties[vocab.LastWriteTimeUtc] = value.LastWriteTimeUtc.ToString("o");

            try
            {
                var parentUri = uriBuilder.GetUri(value.Directory);

                _factory.CreateOutgoingEntityReference(clue, EntityType.Files.Directory, EntityEdgeType.Parent, parentUri, p => p.AbsoluteUri, p => value.Directory.Name);
            }
            catch (PathTooLongException)
            {
            }

            if (input.Owner != null && input.Owner.Sid != null)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.OwnedBy, input.Owner, p => p.Sid.ToString(), p => p?.Principal?.DisplayName ?? p?.Principal?.Name ?? p?.Principal?.SamAccountName ?? p?.Account?.ToString());

//            indexer.Index(value, clue, state);

            return clue;
        }
    }
}
