// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DirectoryInfoVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the DirectoryInfoVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.FileSystem.Vocabularies
{
    /// <summary>The directory info vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class DirectoryInfoVocabulary : SimpleVocabulary
    {
        public DirectoryInfoVocabulary()
        {
            this.VocabularyName = "File System Directory";
            this.KeyPrefix      = "filesystem.directory";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Files.Directory;

            this.IsReadOnly         = this.Add(new VocabularyKey("isReadOnly"));
            this.IsHidden           = this.Add(new VocabularyKey("isHidden"));
            this.IsArchive          = this.Add(new VocabularyKey("isArchive"));
            this.IsSystem           = this.Add(new VocabularyKey("isSystem"));
            this.CreationTimeUtc    = this.Add(new VocabularyKey("creationTimeUtc"));
            this.LastWriteTimeUtc   = this.Add(new VocabularyKey("lastWriteTimeUtc"));

            this.AddMapping(this.CreationTimeUtc, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.CreatedDate);
            this.AddMapping(this.LastWriteTimeUtc, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInDates.ModifiedDate);
        }

        public VocabularyKey IsReadOnly { get; protected set; }

        public VocabularyKey IsHidden { get; protected set; }

        public VocabularyKey IsArchive { get; protected set; }

        public VocabularyKey IsSystem { get; protected set; }

        public VocabularyKey CreationTimeUtc { get; protected set; }

        public VocabularyKey LastWriteTimeUtc { get; protected set; }
    }
}