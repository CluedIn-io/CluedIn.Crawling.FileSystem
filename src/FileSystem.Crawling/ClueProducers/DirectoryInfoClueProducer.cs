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
using CluedIn.Crawling.FileSystem.UriBuilders;
using RuleConstants = CluedIn.Core.Constants.Validation.Rules;

namespace CluedIn.Crawling.FileSystem.ClueProducers
{
    public class DirectoryInfoClueProducer : BaseClueProducer<FileSystemItem<DirectoryInfo>>
    {
        private readonly IClueFactory _factory;
        private readonly FileSystemCrawlJobData _crawlJobData;

        public DirectoryInfoClueProducer(IClueFactory factory, FileSystemCrawlJobData crawlJobData)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _crawlJobData = crawlJobData ?? throw new ArgumentNullException(nameof(crawlJobData));
        }

        protected override Clue MakeClueImpl(FileSystemItem<DirectoryInfo> input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var value = input.Item;
            var uriBuilder = new FileSystemInfoUriBuilder();

            var clue = _factory.Create(EntityType.Files.Directory, uriBuilder.GetUri(value).AbsoluteUri, accountId);

            var data = clue.Data.EntityData;

            var uri = uriBuilder.GetUri(value);

            data.Name = value.Name; // + crawl.FileSystemInfo.Extension;
            data.DisplayName = value.Name;
            data.Uri = uri;
            data.Culture = CultureInfo.InvariantCulture;

            var attributes = value.Attributes;

            var isReadOnly = (attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
            var isHidden = (attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
            var isArchive = (attributes & FileAttributes.Archive) == FileAttributes.Archive;
            var isSystem = (attributes & FileAttributes.System) == FileAttributes.System;

            var vocab = new DirectoryInfoVocabulary();
            var securityVocab = new FileSecurityVocabulary();

            data.Properties[vocab.IsReadOnly] = isReadOnly.ToString();
            data.Properties[vocab.IsHidden] = isHidden.ToString();
            data.Properties[vocab.IsArchive] = isArchive.ToString();
            data.Properties[vocab.IsSystem] = isSystem.ToString();

            data.Properties[securityVocab.OwnerSID] = input.Owner.PrintIfAvailable(v => v.Sid.ToString());
            data.Properties[securityVocab.OwnerNTAccount] = input.Owner.PrintIfAvailable(v => v.Account?.ToString());
            data.Properties[securityVocab.OwnerDisplayName] = input.Owner.PrintIfAvailable(v => v.Principal?.DisplayName);

            data.Properties[vocab.CreationTimeUtc] = value.CreationTimeUtc.ToString("o");
            data.Properties[vocab.LastWriteTimeUtc] = value.LastWriteTimeUtc.ToString("o");


            clue.ValidationRuleSuppressions.AddRange(new[]
            {
                RuleConstants.METADATA_002_Uri_MustBeSet
            });

            try
            {
                if (IsDirectoryInfoEntry(input, value.FullName))
                {
                    _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.Host.Computer, EntityEdgeType.Parent, Environment.MachineName, p => p);
                }
                else if (value.Parent != null)
                {
                    var parentUri = uriBuilder.GetUri(value.Parent);
                    _factory.CreateOutgoingEntityReference(clue, EntityType.Files.Directory, EntityEdgeType.Parent, parentUri, p => p.AbsoluteUri, p => value.Parent.Name);
                }
            }
            catch (PathTooLongException)
            {
            }

            if (input.Owner != null && input.Owner.Sid != null)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Infrastructure.User, EntityEdgeType.OwnedBy, input.Owner, p => p.Sid.ToString(), p => p?.Principal?.DisplayName ?? p?.Principal?.Name ?? p?.Principal?.SamAccountName ?? p?.Account?.ToString());

            return clue;
        }

        protected bool IsDirectoryInfoEntry(FileSystemItem<DirectoryInfo> input, string name)
        {
            return input._startingPoints.Any(p => new DirectoryInfo(p.EntryPoint).FullName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
