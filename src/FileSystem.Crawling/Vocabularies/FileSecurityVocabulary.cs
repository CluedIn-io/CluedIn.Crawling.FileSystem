// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileSecurityVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FileSecurityVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.FileSystem.Vocabularies
{
    /// <summary>The file security vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class FileSecurityVocabulary : SimpleVocabulary
    {
        public FileSecurityVocabulary()
        {
            this.VocabularyName = "File System File Security";
            this.KeyPrefix      = "filesystem.file.security";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Unknown;

            this.OwnerSID           = this.Add(new VocabularyKey("owner.sid", VocabularyKeyVisibility.Hidden));
            this.OwnerNTAccount     = this.Add(new VocabularyKey("owner.ntAccount"));
            this.OwnerDisplayName   = this.Add(new VocabularyKey("owner.displayName"));
        }

        public VocabularyKey OwnerSID { get; protected set; }

        public VocabularyKey OwnerNTAccount { get; protected set; }

        public VocabularyKey OwnerDisplayName { get; protected set; }
    }
}