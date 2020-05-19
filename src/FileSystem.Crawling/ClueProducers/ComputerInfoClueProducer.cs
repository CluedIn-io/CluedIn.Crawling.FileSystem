using System;
using System.Globalization;
using System.Net;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.FileSystem.Core;
using CluedIn.Crawling.Helpers;

using CluedIn.Crawling.FileSystem.Vocabularies;
using CluedIn.Crawling.FileSystem.Core.Models;
using RuleConstants = CluedIn.Core.Constants.Validation.Rules;

using Microsoft.VisualBasic.Devices;

namespace CluedIn.Crawling.FileSystem.ClueProducers
{
    public class ComputerInfoClueProducer : BaseClueProducer<ComputerInfo>
    {
        private readonly IClueFactory _factory;

        public ComputerInfoClueProducer(IClueFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        protected override Clue MakeClueImpl(ComputerInfo input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // TODO: Create clue specifying the type of entity it is and ID
            var clue = _factory.Create(EntityType.Infrastructure.Host.Computer, Environment.MachineName, accountId);

            var data = clue.Data.EntityData;
            data.Name = FileSystemConstants.CrawlerName;
            data.Uri = new Uri(FileSystemConstants.Uri);
            data.Description = FileSystemConstants.CrawlerDescription;

            data.Name = Environment.MachineName;
            data.Properties[FileSystemVocabulary.ComputerInfo.OperatingSystem] = Environment.OSVersion.ToString();
            data.Properties[FileSystemVocabulary.ComputerInfo.Architecture] = Environment.Is64BitOperatingSystem ? "x64" : "x86";
            data.Properties[FileSystemVocabulary.ComputerInfo.Processors] = Environment.ProcessorCount.ToString();
            data.Properties[FileSystemVocabulary.ComputerInfo.TotalPhysicalMemory] = (input.TotalPhysicalMemory / 1024 / 1024).ToString(CultureInfo.InvariantCulture);
            data.Properties[FileSystemVocabulary.ComputerInfo.TotalVirtualMemory] = (input.TotalVirtualMemory / 1024 / 1024).ToString(CultureInfo.InvariantCulture);
            data.Properties[FileSystemVocabulary.ComputerInfo.RunTimeVersion] = Environment.Version.ToString();
            data.Properties[FileSystemVocabulary.ComputerInfo.ImageRunTimeVersion] = typeof(ServerComponentHost).Assembly.ImageRuntimeVersion;
            data.Properties[FileSystemVocabulary.ComputerInfo.HostName] = Dns.GetHostName();

            data.Uri = new Uri(string.Format("{0}{1}{2}", Uri.UriSchemeFile, Uri.SchemeDelimiter, Environment.MachineName));
            clue.Data.EntityData.OutgoingEdges.Add(new EntityEdge(new EntityReference(new EntityCode(EntityType.Infrastructure.Host.Computer, FileSystemConstants.CodeOrigin, Environment.MachineName)), new EntityReference(new EntityCode(EntityType.Organization, CodeOrigin.CluedIn, clue.OrganizationId)), EntityEdgeType.PartOf));
            
            _factory.CreateEntityRootReference(clue, EntityEdgeType.ManagedIn);

            clue.ValidationRuleSuppressions.AddRange(new[]
            {
                RuleConstants.METADATA_002_Uri_MustBeSet
            });

            return clue;
        }
    }
}
