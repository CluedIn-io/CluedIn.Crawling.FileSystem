// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComputerInfoVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the ComputerInfoVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.Crawling.FileSystem.Vocabularies
{
    /// <summary>The computer information vocabulary</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class ComputerInfoVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComputerInfoVocabulary"/> class.
        /// </summary>
        public ComputerInfoVocabulary()
        {
            this.VocabularyName = "File System Computer";
            this.KeyPrefix      = "filesystem.computer";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Infrastructure.Host.Computer;

            this.OperatingSystem        = this.Add(new VocabularyKey("OperatingSystem"));
            this.Architecture           = this.Add(new VocabularyKey("Architecture"));
            this.Processors             = this.Add(new VocabularyKey("Processors"));
            this.TotalPhysicalMemory    = this.Add(new VocabularyKey("TotalPhysicalMemory"));
            this.TotalVirtualMemory     = this.Add(new VocabularyKey("TotalVirtualMemory"));
            this.RunTimeVersion         = this.Add(new VocabularyKey("RunTimeVersion"));
            this.ImageRunTimeVersion    = this.Add(new VocabularyKey("ImageRunTimeVersion"));
            this.HostName               = this.Add(new VocabularyKey("HostName"));

            this.AddMapping(this.OperatingSystem,       CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInComputer.OperatingSystem);
            this.AddMapping(this.Architecture,          CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInComputer.Architecture);
            this.AddMapping(this.Processors,            CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInComputer.Processors);
            this.AddMapping(this.TotalPhysicalMemory,   CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInComputer.TotalPhysicalMemory);
            this.AddMapping(this.TotalVirtualMemory,    CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInComputer.TotalVirtualMemory);
            this.AddMapping(this.RunTimeVersion,        CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInComputer.RunTimeVersion);
            this.AddMapping(this.ImageRunTimeVersion,   CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInComputer.ImageRunTimeVersion);
            this.AddMapping(this.HostName,              CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInComputer.HostName);
        }

        public VocabularyKey OperatingSystem { get; protected set; }
        public VocabularyKey Architecture { get; protected set; }
        public VocabularyKey Processors { get; protected set; }
        public VocabularyKey TotalPhysicalMemory { get; protected set; }
        public VocabularyKey TotalVirtualMemory { get; protected set; }
        public VocabularyKey RunTimeVersion { get; protected set; }
        public VocabularyKey ImageRunTimeVersion { get; protected set; }
        public VocabularyKey HostName { get; protected set; }
    }
}