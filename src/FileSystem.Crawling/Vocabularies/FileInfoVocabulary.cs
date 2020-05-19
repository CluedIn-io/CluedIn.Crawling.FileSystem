// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileInfoVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FileInfoVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.FileSystem.Vocabularies
{
    /// <summary>The file info vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class FileInfoVocabulary : SimpleVocabulary
    {
        public FileInfoVocabulary()
        {
            this.VocabularyName = "File System File";
            this.KeyPrefix      = "filesystem.file";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Files.File;

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