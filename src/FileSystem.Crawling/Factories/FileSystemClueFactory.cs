using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.FileSystem.Core;
using RuleConstants = CluedIn.Core.Constants.Validation.Rules;

namespace CluedIn.Crawling.FileSystem.Factories
{
  public class FileSystemClueFactory : ClueFactory
  {
    public FileSystemClueFactory()
        : base(FileSystemConstants.CodeOrigin, FileSystemConstants.ProviderRootCodeValue)
    {
    }

    protected override Clue ConfigureProviderRoot([NotNull] Clue clue)
    {
      if (clue == null) throw new ArgumentNullException(nameof(clue));

      var data = clue.Data.EntityData;
      data.Name = FileSystemConstants.CrawlerName;
      data.Uri = new Uri(FileSystemConstants.Uri);
      data.Description = FileSystemConstants.CrawlerDescription;

      // TODO this should not be necessary
      clue.ValidationRuleSuppressions.AddRange(new[]
          {
            RuleConstants.METADATA_002_Uri_MustBeSet
          });

      return clue;
    }
  }
}
